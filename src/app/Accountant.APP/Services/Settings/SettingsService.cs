using Accountant.APP.Services.Settings.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Accountant.APP.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        private const string _AccessToken = "access_token";
        private const string _UserId = "user_id";
        private const string _GroupId = "group_id";

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        public string AuthToken
        {
            get => GetValueOrDefaultInternal(_AccessToken, string.Empty);
            set => AddOrUpdateValueInternal(_AccessToken, value);
        }

        public int? UserId
        {
            get => GetValueOrDefaultInternal<int?>(_UserId);
            set => AddOrUpdateValueInternal(_UserId, value);
        }

        public int? GroupId
        {
            get => GetValueOrDefaultInternal<int?>(_GroupId);
            set => AddOrUpdateValueInternal(_GroupId, value);
        }
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

        async Task AddOrUpdateValueInternal<T>(string key, T value)
        {
            if (value == null)
            {
                await Remove(key);
            }

            Application.Current.Properties[key] = value;
            try
            {
                await Application.Current.SavePropertiesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to save: " + key, " Message: " + ex.Message);
            }
        }

        T GetValueOrDefaultInternal<T>(string key, T defaultValue = default(T))
        {
            object value = null;
            if (Application.Current.Properties.ContainsKey(key))
            {
                value = Application.Current.Properties[key];
            }
            return null != value ? (T)value : defaultValue;
        }

        async Task Remove(string key)
        {
            try
            {
                if (Application.Current.Properties[key] != null)
                {
                    Application.Current.Properties.Remove(key);
                    await Application.Current.SavePropertiesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to remove: " + key, " Message: " + ex.Message);
            }
        }
    }
}
