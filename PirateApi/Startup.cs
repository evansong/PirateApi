﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PirateApi.Startup))]

namespace PirateApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
