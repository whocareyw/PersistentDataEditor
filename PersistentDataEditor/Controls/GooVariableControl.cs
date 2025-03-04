﻿using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Parameters.Hints;
using Grasshopper.Kernel.Types;
using System;

namespace PersistentDataEditor;

internal class GooVariableControl : GooHorizonalControlBase<IGH_Goo>
{
    public override Guid AddCompnentGuid => _values[0].AddCompnentGuid;

    private readonly Param_ScriptVariable _owner;
    public GooVariableControl(Func<IGH_Goo> valueGetter, Func<bool> isNull, Param_ScriptVariable owner) : base(valueGetter, isNull, null)
    {
        owner.ObjectChanged += Owner_ObjectChanged;
        _owner = owner;
        ChangeControlItems();
    }

    private void Owner_ObjectChanged(IGH_DocumentObject sender, GH_ObjectChangedEventArgs e)
    {
        if (e.Tag is IGH_TypeHint)
        {
            //For some case, the value type have changed. So the Value must be null for avoiding casting exception.
            ShowValue = null;

            ChangeControlItems();
        }
    }

    protected override BaseControlItem[] SetControlItems()
    {
        if (_owner != null && _owner.TypeHint != null)
        {
            //if (script.TypeHint is GH_ArcHint)
            //    guid = new Param_Arc().ComponentGuid;
            if (ShouldUse<GH_Boolean>() && (_owner.TypeHint is GH_BooleanHint_CS || _owner.TypeHint is GH_BooleanHint_VB))
                return [
                    new GooBooleanControl(() => (GH_Boolean)ShowValue, _isNull),
                ];
            else if (ShouldUse<GH_Box>() && _owner.TypeHint is GH_BoxHint)
                return [
                    new GooBoxControl(() => (GH_Box)ShowValue, _isNull, null),
                ];
            //else if (script.TypeHint is GH_BrepHint)
            //    guid = new Param_Brep().ComponentGuid;
            else if (ShouldUse<GH_Circle>() && _owner.TypeHint is GH_CircleHint)
                return [
                    new GooCircleControl(() => (GH_Circle)ShowValue, _isNull, null),
                ];
            else if (ShouldUse<GH_Colour>() && _owner.TypeHint is GH_ColorHint)
                return [
                    new GooColorControl(() => (GH_Colour)ShowValue, _isNull),
                ];
            else if (ShouldUse<GH_ComplexNumber>() && _owner.TypeHint is GH_ComplexHint)
                return [
                    new GooComplexControl(() => (GH_ComplexNumber)ShowValue, _isNull, null),
                ];
            //else if (script.TypeHint is GH_CurveHint)
            //    guid = new Param_Curve().ComponentGuid;
            //else if (script.TypeHint is GH_DateTimeHint)
            //    guid = new Param_Time().ComponentGuid;
            else if (ShouldUse<GH_Number>() && (_owner.TypeHint is GH_DoubleHint_CS || _owner.TypeHint is GH_DoubleHint_VB))
                return [
                    new GooNumberControl(() =>
                    {
                        if (ShowValue is GH_Integer integer)
                        {
                            return new GH_Number(integer.Value);
                        }
                        else if (ShowValue is GH_String str)
                        {
                            if (double.TryParse(str.Value, out double value))
                            {
                                return new GH_Number(value);
                            }
                            return null;
                        }
                        else
                        {
                            return (GH_Number)ShowValue;
                        }
                    }, _isNull, null),
                ];

            //else if (script.TypeHint is GH_GeometryBaseHint)
            //    guid = new Param_Geometry().ComponentGuid;
            //else if (script.TypeHint is GH_GuidHint)
            //    guid = new Param_Guid().ComponentGuid;
            else if (ShouldUse<GH_Integer>() && (_owner.TypeHint is GH_IntegerHint_CS || _owner.TypeHint is GH_IntegerHint_VB))
                return [
                    new GooIntegerControl(() =>
                    {
                        if (ShowValue is GH_Number number)
                        {
                            return new GH_Integer((int)number.Value);
                        }
                        else if (ShowValue is GH_String str)
                        {
                            if (int.TryParse(str.Value, out var value))
                            {
                                return new GH_Integer(value);
                            }
                            return null;
                        }
                        else
                        {
                            return (GH_Integer)ShowValue;
                        }
                    }, _isNull, null),
                ];
            else if (ShouldUse<GH_Interval>() && _owner.TypeHint is GH_IntervalHint)
                return [
                    new GooIntervalControl(() => (GH_Interval)ShowValue, _isNull, null),
                ];
            else if (ShouldUse<GH_Line>() && _owner.TypeHint is GH_LineHint)
                return [
                    new GooLineControl(() => (GH_Line)ShowValue, _isNull, null),
                ];
            //else if (script.TypeHint is GH_MeshHint)
            //    guid = new Param_Mesh().ComponentGuid;
            //else if (script.TypeHint is GH_NullHint)
            //    guid = new Param_GenericObject().ComponentGuid;
            else if (ShouldUse<GH_Plane>() && _owner.TypeHint is GH_PlaneHint)
                return [
                    new GooPlaneControl(() => (GH_Plane)ShowValue, _isNull, null),
                ];
            else if (ShouldUse<GH_Point>() && _owner.TypeHint is GH_Point3dHint)
                return [
                    new GooPointControl(() => (GH_Point)ShowValue, _isNull, null),
                ];
            //else if (script.TypeHint is GH_PolylineHint)
            //    guid = new Param_Curve().ComponentGuid;
            else if (ShouldUse<GH_Rectangle>() && _owner.TypeHint is GH_Rectangle3dHint)
                return [
                    new GooRectangleControl(() => (GH_Rectangle)ShowValue, _isNull, null),
                ];
            else if (ShouldUse<GH_String>() && (_owner.TypeHint is GH_StringHint_CS || _owner.TypeHint is GH_StringHint_VB))
                return [
                    new GooInputBoxStringControl<GH_String>(() => (GH_String)ShowValue, _isNull),
                ];
            //else if (script.TypeHint?.TypeName == "SubD")
            //    guid = new Guid("{89CD1A12-0007-4581-99BA-66578665E610}");
            //else if (script.TypeHint is GH_SurfaceHint)
            //    guid = new Param_Surface().ComponentGuid;
            //else if (script.TypeHint is GH_TransformHint)
            //    guid = new Param_Transform().ComponentGuid;
            else if (ShouldUse<GH_Interval2D>() && _owner.TypeHint is GH_UVIntervalHint)
                return [
                    new GooInterval2DControl(() => (GH_Interval2D)ShowValue, _isNull, null),
                ];
            else if (ShouldUse<GH_Vector>() && _owner.TypeHint is GH_Vector3dHint)
                return [
                    new GooVectorControl(() => (GH_Vector)ShowValue, _isNull, null),
                ];
            else return [new GooGeneralControl<IGH_Goo>(() => ShowValue, _isNull)];
        }
        else return [new GooGeneralControl<IGH_Goo>(() => ShowValue, _isNull)];
    }

    private static bool ShouldUse<T>() where T : class, IGH_Goo
    {
        string saveBooleanKey = "UseParam" + typeof(T).Name;
        return Instances.Settings.GetValue(saveBooleanKey, true);
    }

    protected override IGH_Goo SetValue(IGH_Goo[] values)
    {
        return values[0];
    }

    public override void DosomethingWhenCreate(IGH_DocumentObject obj)
    {
        _values[0].DosomethingWhenCreate(obj);
    }
}
