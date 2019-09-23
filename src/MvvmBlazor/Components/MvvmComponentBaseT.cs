﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MvvmBlazor.Extensions;
using MvvmBlazor.ViewModel;

namespace MvvmBlazor.Components
{
    public abstract class MvvmComponentBase<T> : MvvmComponentBase where T : ViewModelBase
    {
        protected internal T BindingContext { get; set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public MvvmComponentBase()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            SetBindingContext();
        }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        internal MvvmComponentBase(IDependencyResolver dependencyResolver) : base(dependencyResolver)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            SetBindingContext();
        }

        private void SetBindingContext()
        {
            BindingContext = Resolver.GetService<T>();
        }

        protected internal TValue Bind<TValue>(Expression<Func<T, TValue>> property)
        {
            return AddBinding(BindingContext, property);
        }

        #region Lifecycle Methods

        /// <inheritdoc />
        protected sealed override void OnInitialized()
        {
            BindingContext?.OnInitialized();
        }

        /// <inheritdoc />
        protected sealed override Task OnInitializedAsync()
        {
            return BindingContext?.OnInitializedAsync() ?? Task.CompletedTask;
        }

        /// <inheritdoc />
        protected sealed override void OnParametersSet()
        {
            BindingContext?.OnParametersSet();
        }

        /// <inheritdoc />
        protected sealed override Task OnParametersSetAsync()
        {
            return BindingContext?.OnParametersSetAsync() ?? Task.CompletedTask;
        }

        /// <inheritdoc />
        protected sealed override bool ShouldRender()
        {
            return BindingContext?.ShouldRender() ?? true;
        }

        /// <inheritdoc />
        protected sealed override void OnAfterRender(bool firstRender)
        {
            BindingContext?.OnAfterRender(firstRender);
        }

        /// <inheritdoc />
        protected sealed override Task OnAfterRenderAsync(bool firstRender)
        {
            return BindingContext?.OnAfterRenderAsync(firstRender) ?? Task.CompletedTask;
        }

        /// <inheritdoc />
        public sealed override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(false);

            if (BindingContext != null)
                await BindingContext.SetParametersAsync(parameters).ConfigureAwait(false);
        }

        #endregion
    }
}