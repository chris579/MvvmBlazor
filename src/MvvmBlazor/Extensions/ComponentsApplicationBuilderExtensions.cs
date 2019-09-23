﻿using System;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using MvvmBlazor.Components;

namespace MvvmBlazor.Extensions
{
    public static class ComponentsApplicationBuilderExtensions
    {
        public static IComponentsApplicationBuilder UseMvvm(
            this IComponentsApplicationBuilder componentsApplicationBuilder)
        {
            if (componentsApplicationBuilder == null)
                throw new ArgumentNullException(nameof(componentsApplicationBuilder));

            var dependencyResolver = componentsApplicationBuilder.Services.GetRequiredService<IDependencyResolver>();
            DependencyResolver.Default = dependencyResolver;

            return componentsApplicationBuilder;
        }
    }
}