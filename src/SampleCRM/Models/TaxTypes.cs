﻿using OpenRiaServices.Client;

namespace SampleCRM.Web.Models
{
    public partial class TaxTypes : Entity
    {
        public string Desc => $"{Name} ({Rate})";
    }
}