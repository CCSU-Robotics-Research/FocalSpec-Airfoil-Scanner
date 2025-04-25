// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecordContainer.cs" company="FocalSpec Oy">
//   FocalSpec Oy 2016-
// </copyright>
// <summary>
//   Container class for recording points from the camera.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System.Linq;

namespace FocalSpec.GuiExample.Model.BatchMode
{
    using System.Collections.Generic;
    using FsApiNet.Model;
    using Camera;

    /// <summary>
    /// Container class for recording points from the camera.
    /// </summary>
    public class RecordContainer
    {
        /// <summary>
        /// The maximum length for the recording queue.
        /// </summary>
        private readonly int _maxLength;

        /// <summary>
        /// Queue for the profiles received from the camera.
        /// </summary>
        private readonly Dictionary<int, List<Profile>> _recordingQueue = new Dictionary<int, List<Profile>>();

        /// <summary>
        /// Sets the max. batch length.
        /// </summary>
        /// <param name="maxLength">The length for the recording. Default value is the value set in defines.</param>
        public RecordContainer(int maxLength)
        {
            _maxLength = maxLength;
        }

        /// <summary>
        /// Gets a value indicating whether all profiles are already collected into queue.
        /// </summary>
        /// <value>True, if collecting is ready.</value>
        public bool IsCollected => Count == _maxLength;

        /// <summary>
        /// Gets the number of profiles in recording queue.
        /// </summary>
        public int Count
        {
            get
            {
                if (_recordingQueue.Count <= 0) return 0;
                if (_recordingQueue.ContainsKey(-1)) return _recordingQueue[-1].Count;
                if (_recordingQueue.ContainsKey(0)) return _recordingQueue[0].Count;
                return _recordingQueue.First().Value.Count;
            }
        }

        /// <summary>
        /// Adds the points to recording queue.
        /// </summary>
        /// <param name="profile">The received profile.</param>
        /// <returns>True if points were added.</returns>
        public bool AddProfile(Profile profile)
        {
            if (Count >= _maxLength) return false;

            if (!_recordingQueue.ContainsKey(profile.LayerId))
                _recordingQueue[profile.LayerId] = new List<Profile>();
			// discard if 1st profile is most likely old profile from previous run
	        if (Count == 0 && profile.Header.Index > 10)
		        return true;
            
            _recordingQueue[profile.LayerId].Add(profile);
            return true;
        }

        /// <summary>
        /// Gets a profile from the container. If the requested index doesn't exit, return empty profile.
        /// </summary>
        /// <param name="layer">Zero-based index of the profile layer id. </param>
        /// <param name="index">Zero-based index of the profile in the container. </param>
        /// <returns>The profile from the container.</returns>
        public Profile GetProfile(int layer, int index)
        {
            if (_recordingQueue != null && _recordingQueue.ContainsKey(layer) && _recordingQueue[layer].Count > index)
            {
                return _recordingQueue[layer][index];
            }

            return new Profile(new List<FsApi.Point>(),new FsApi.Header());
        }

        /// <summary>
        /// Gets the profiles.
        /// </summary>
        /// <returns>List of profiles.</returns>
        public Dictionary<int, List<Profile>> GetProfiles()
        {
            return _recordingQueue;
        }

        /// <summary>
        /// Gets the profiles.
        /// </summary>
        /// <param name="layer">Zero-based index of the profile layer id. </param>
        /// <returns>List of profiles.</returns>
        public List<Profile> GetProfiles(int layer)
        {
            return _recordingQueue == null || !_recordingQueue.ContainsKey(layer) ? null : _recordingQueue[layer];
        }

        /// <summary>
        /// Gets the min and max Z values from the profiles.
        /// </summary>
        /// <param name="minZ">Min. Z value [µm] found.</param>
        /// <param name="maxZ">Max. Z value [µm] found.</param>
        public void GetProfilesMinAndMaxZ(out double minZ, out double maxZ)
        {
            minZ = double.NaN;
            maxZ = double.NaN;

            var profiles = GetProfiles();

            if (profiles.Count <= 0) return;

            double min = double.MaxValue;
            double max = double.MinValue;

            foreach (var layer in profiles.Keys)
            {
                for (int profileIndex = 0; profileIndex < profiles[layer].Count; profileIndex++)
                {
                    Profile profile = profiles[layer][profileIndex];
                    if (layer < 0)
                    {
                        foreach (var profilePoint in profile.Points)
                        {
                            if (profilePoint.Y < min)
                                min = profilePoint.Y;
                            if (profilePoint.Y > max)
                                max = profilePoint.Y;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < profile.LineLength; ++i)
                        {
                            if (profile.ZValues[i] > FsApi.NoMeasurement - 1)
                                continue;

                            if (profile.ZValues[i] < min)
                                min = profile.ZValues[i];
                            if (profile.ZValues[i] > max)
                                max = profile.ZValues[i];
                        }
                    }
                }
            }

            minZ = min;
            maxZ = max;
        }
    }
}