﻿using System.IO;

namespace DatingApp.Helper
{
    public class UserParams
    {
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;
        private int _PageSize = 5; //default Page size

        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value > MaxPageSize )? MaxPageSize : value; 
        }

        public string? CurrentUserName { get; set; }
        public string? Gender  { get; set; }

        public int MinAge { get; set; } = 18;

        public int MaxAge { get; set; } = 150;
        public string OrderBy { get; set; } = "lastActive";

    }
}
