﻿using System;

namespace WcfSelfHosting
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string[] Authors { get; set; }
    }
}
