﻿using Microsoft.AspNetCore.Components;
using System;

namespace Blazor.GoogleMap.Components.Rendering
{
    public interface IRendererTreeBuilder
    {
        IRendererTreeBuilder OpenElement(string elementName);

        IRendererTreeBuilder CloseElement();

        IRendererTreeBuilder AddAttribute(string name, object value);

        IRendererTreeBuilder AddAttribute(string name, bool value);

        IRendererTreeBuilder AddAttribute(string name, string value);

        IRendererTreeBuilder AddAttribute(string name, MulticastDelegate value);

        IRendererTreeBuilder AddAttribute(string name, Func<MulticastDelegate> value);

        IRendererTreeBuilder AddAttribute(string name, Action<EventArgs> value);

        IRendererTreeBuilder AddContent(string textContent);

        IRendererTreeBuilder AddContent(RenderFragment fragment);

        IRendererTreeBuilder AddContent(MarkupString markupContent);

        IRendererTreeBuilder OpenComponent(Type componentType);

        IRendererTreeBuilder AddOnClickEvent(Func<MulticastDelegate> onClickBindMethod);

        IRendererTreeBuilder CloseComponent();
    }
}
