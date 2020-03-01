﻿using Blazorade.Core.Components.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazorade.Bootstrap.Components.Builder
{

    /// <summary>
    /// A class builder that supports generic Bootstrap classes that can be applied to more or less any element.
    /// </summary>
    public class ComponentClassBuilder : ClassBuilder, IComponentFeatureBuilder, IComponentBreakpointBuilder, IClassBuilder
    {

        private List<string> PendingFeatures = new List<string>();


        /// <summary>
        /// Builds the classes and returns them as a string collection.
        /// </summary>
        public override IEnumerable<string> Build()
        {
            this.FlushPendingFeatures();
            return base.Build();
        }



        /// <summary>
        /// Sets the background color of the element to the given colour.
        /// </summary>
        public IComponentFeatureBuilder Background(NamedColor color)
        {
            this.AddPendingFeature($"bg-{color.ToString().Hyphenate().ToLower()}");
            return this;
        }

        /// <summary>
        /// Sets the background of the element to transparent.
        /// </summary>
        public IComponentFeatureBuilder BackgroundTransparent()
        {
            this.AddPendingFeature("bg-transparent");
            return this;
        }




        /// <summary>
        /// Specifies a border with the given value.
        /// </summary>
        /// <param name="value">The border value. If not specified, adds a border on all sides.</param>
        public IComponentFeatureBuilder Border(string value = null)
        {
            this.AddPendingFeature($"border-{value?.Hyphenate()?.ToLower()}");
            return this;
        }

        /// <summary>
        /// Adds a border to the top.
        /// </summary>
        public IComponentFeatureBuilder BorderTop()
        {
            this.Border("top");
            return this;
        }

        /// <summary>
        /// Adds a border to the right.
        /// </summary>
        public IComponentFeatureBuilder BorderRight()
        {
            this.Border("right");
            return this;
        }

        /// <summary>
        /// Adds a border at the bottom.
        /// </summary>
        public IComponentFeatureBuilder BorderBottom()
        {
            this.Border("bottom");
            return this;
        }

        /// <summary>
        /// Adds a border to the left.
        /// </summary>
        public IComponentFeatureBuilder BorderLeft()
        {
            this.Border("left");
            return this;
        }

        /// <summary>
        /// Removes all borders.
        /// </summary>
        public IComponentFeatureBuilder Border0()
        {
            this.Border("0");
            return this;
        }

        /// <summary>
        /// Removes the top border.
        /// </summary>
        public IComponentFeatureBuilder Border0Top()
        {
            this.Border("top-0");
            return this;
        }

        /// <summary>
        /// Removes the right border.
        /// </summary>
        public IComponentFeatureBuilder Border0Right()
        {
            this.Border("right-0");
            return this;
        }

        /// <summary>
        /// Removes the bottom border.
        /// </summary>
        public IComponentFeatureBuilder Border0Bottom()
        {
            this.Border("bottom-0");
            return this;
        }

        /// <summary>
        /// Removes the left border.
        /// </summary>
        public IComponentFeatureBuilder Border0Left()
        {
            this.Border("left-0");
            return this;
        }

        /// <summary>
        /// Specifies the colour of the border.
        /// </summary>
        public IComponentFeatureBuilder Border(NamedColor color)
        {
            this.Border(color.ToString());
            return this;
        }



        /// <summary>
        /// Sets the display to the given value.
        /// </summary>
        public IComponentBreakpointBuilder Display(string value)
        {
            this.AddPendingFeature($"d-{{0}}-{value}");
            return this;
        }

        /// <summary>
        /// Display: none
        /// </summary>
        public IComponentBreakpointBuilder DisplayNone()
        {
            this.Display("none");
            return this;
        }

        /// <summary>
        /// Display: inline
        /// </summary>
        public IComponentBreakpointBuilder DisplayInline()
        {
            this.Display("inline");
            return this;
        }

        /// <summary>
        /// Display: inline-block
        /// </summary>
        public IComponentBreakpointBuilder DisplayInlineBlock()
        {
            this.Display("inline-block");
            return this;
        }

        /// <summary>
        /// Display: block
        /// </summary>
        public IComponentBreakpointBuilder DisplayBlock()
        {
            this.Display("block");
            return this;
        }

        /// <summary>
        /// Display: table
        /// </summary>
        public IComponentBreakpointBuilder DisplayTable()
        {
            this.Display("table");
            return this;
        }

        /// <summary>
        /// Display: table-cell
        /// </summary>
        public IComponentBreakpointBuilder DisplayTableCell()
        {
            this.Display("table-cell");
            return this;
        }

        /// <summary>
        /// Display: table-row
        /// </summary>
        public IComponentBreakpointBuilder DisplayTableRow()
        {
            this.Display("table-row");
            return this;
        }

        /// <summary>
        /// Display: flex
        /// </summary>
        public IComponentBreakpointBuilder DisplayFlex()
        {
            this.Display("flex");
            return this;
        }

        /// <summary>
        /// Display: inline-flex
        /// </summary>
        public IComponentBreakpointBuilder DisplayInlineFlex()
        {
            this.Display("inline-flex");
            return this;
        }



        /// <summary>
        /// Sets the text colour to the given colour.
        /// </summary>
        public IComponentFeatureBuilder Text(NamedColor color)
        {
            this.AddPendingFeature($"text-{color.ToString().Hyphenate().ToLower()}");
            return this;
        }

        /// <summary>
        /// Sets the text colour to the body text colour.
        /// </summary>
        public IComponentFeatureBuilder TextBody()
        {
            this.AddPendingFeature("text-body");
            return this;
        }

        /// <summary>
        /// Sets the text colour to a muted colour.
        /// </summary>
        public IComponentFeatureBuilder TextMuted()
        {
            this.Add("text-muted");
            return this;
        }



        IComponentFeatureBuilder IResponsiveBreakpointBuilder<IComponentFeatureBuilder>.OnAll()
        {
            this.FlushPendingFeatures();
            return this;
        }

        IComponentFeatureBuilder IResponsiveBreakpointBuilder<IComponentFeatureBuilder>.OnLg()
        {
            this.FlushPendingFeatures(ResponsiveBreakpoint.LG);
            return this;
        }

        IComponentFeatureBuilder IResponsiveBreakpointBuilder<IComponentFeatureBuilder>.OnMd()
        {
            this.FlushPendingFeatures(ResponsiveBreakpoint.MD);
            return this;
        }

        IComponentFeatureBuilder IResponsiveBreakpointBuilder<IComponentFeatureBuilder>.OnSm()
        {
            this.FlushPendingFeatures(ResponsiveBreakpoint.SM);
            return this;
        }

        IComponentFeatureBuilder IResponsiveBreakpointBuilder<IComponentFeatureBuilder>.OnXl()
        {
            this.FlushPendingFeatures(ResponsiveBreakpoint.XL);
            return this;
        }



        /// <summary>
        /// Adds a feature class to the builder as a pending feature class.
        /// </summary>
        /// <param name="featureClass">The feature class to add.</param>
        /// <remarks>
        /// A pending feature class is a class that contains a placeholder for a responsive breakpoint, such as <c>col-{0}-6</c>.
        /// The placeholder must always be <c>{0}</c>.
        /// </remarks>
        protected virtual void AddPendingFeature(string featureClass)
        {
            if (!this.PendingFeatures.Contains(featureClass))
            {
                this.PendingFeatures.Add(featureClass);
            }
        }

        /// <summary>
        /// Processes the pending features added in <see cref="AddPendingFeature(string)"/> using the given responsive breakpoint,
        /// and stores them as classes in the current builder.
        /// </summary>
        /// <param name="breakpoint">The breakpoint to use when processing features. If not specified, the features will apply to all breakpoints.</param>
        protected virtual void FlushPendingFeatures(ResponsiveBreakpoint? breakpoint = null)
        {
            foreach (var f in this.PendingFeatures)
            {
                var cls = string.Format(f, breakpoint?.ToString()?.ToLower()).Replace("--", "-");
                if (cls.EndsWith("-")) cls = cls[0..^1];

                this.Add(cls);
            }

            this.PendingFeatures.Clear();
        }

    }
}
