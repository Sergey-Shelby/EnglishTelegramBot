﻿using System.Collections.Generic;

namespace EnglishTelegramBot
{
    public interface IStatusProvider
    {
        void SetStatus(int userId, int statusCode, object details = null);
        UserStatus<T> GetStatus<T>(int userId);
        void ClearStatus(int userId);
    }

    public class StatusProvider : IStatusProvider
    {
        private Dictionary<int, UserStatus<object>> _userStatuses;

        public StatusProvider()
        {
            _userStatuses = new Dictionary<int, UserStatus<object>>();
        }

        public int? GetStatus(int userId)
        {
            _userStatuses.TryGetValue(userId, out var userStatus);
            return userStatus?.StatusCode;
        }

        public UserStatus<T> GetStatus<T>(int userId)
        {
            _userStatuses.TryGetValue(userId, out var userStatus);
            if (userStatus != null)
                return new UserStatus<T> { StatusCode = userStatus.StatusCode, Details = (T)userStatus.Details };
            return null;
        }

        public void SetStatus(int userId, int statusCode, object details = null)
        {
            _userStatuses[userId] = new UserStatus<object> { Details = details, StatusCode = statusCode };
        }

        public void ClearStatus(int userId)
        {
            _userStatuses[userId] = null;
        }
    }

    public class UserStatus<T>
    {
        public int StatusCode { get; set; }
        public T Details { get; set; }
    }

}