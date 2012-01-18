﻿#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using Bifrost.Configuration.ConfigSection;
using Microsoft.Practices.ServiceLocation;

namespace Bifrost.Configuration
{
	/// <summary>
	/// Configuration extension for <see cref="IConfigure"/> configuration for configuring Bifrost for 
	/// a <see cref="IConfigurationSource"/> using a <see cref="IConfigurationManager"/> - typically
	/// a App.config/Web.config section
	/// </summary>
	/// <remarks>
	/// This is part of the fluent interface for configuring Bifrost
	/// </remarks>
    public static partial class ConfigurationExtensions
    {
		/// <summary>
		/// Using the Config configuration source
		/// </summary>
		/// <param name="configure"><see cref="IConfigure"/> instance to configure for</param>
		/// <returns><see cref="IConfigure"/> chain</returns>
        public static IConfigure UsingConfigConfigurationSource(this IConfigure configure)
        {
            configure.ConfigurationSource(new ConfigConfigurationSource(ServiceLocator.Current.GetInstance<IConfigurationManager>()));
            return configure;
        }
    }
}