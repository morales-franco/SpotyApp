﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotyApp.Models
{
    public class NewAlbumReleases
    {
        [JsonProperty("albums")]
        public Albums Albums { get; set; }
    }
}
