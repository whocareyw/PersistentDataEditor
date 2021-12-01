﻿using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentToolkit
{
    internal class ParamColorControl : ParamControlBase<GH_Colour>
    {
        protected override bool Valid => base.Valid && GH_ComponentAttributesReplacer.UseParamColourControl;

        internal ParamColorControl(GH_PersistentParam<GH_Colour> owner) : base(owner)
        {
        }

        protected override BaseControlItem[] SetControlItems(GH_PersistentParam<GH_Colour> owner)
        {
            return new BaseControlItem[]
            {
                new GooColorControl(() => OwnerGooData),
            };
        }
    }
}
