﻿using OneOf;
using pkuManager.Alerts;
using pkuManager.Formats.Fields;
using pkuManager.Formats.pku;
using System.Numerics;
using static pkuManager.Alerts.Alert;
using static pkuManager.Formats.Modules.Gender_Util;
using static pkuManager.Formats.PorterDirective;

namespace pkuManager.Formats.Modules;

public interface OT_Gender_O
{
    public OneOf<IField<BigInteger>, IField<Gender>, IField<Gender?>> OT_Gender { get; }
}

public interface OT_Gender_E : EnumTag_E
{
    public pkuObject pku { get; }
    public OT_Gender_O OT_Gender_Field { get; }

    public Gender? OT_Gender_Default => Gender.Male;
    public bool OT_Gender_AlertIfUnspecified => true;

    [PorterDirective(ProcessingPhase.FirstPass)]
    protected void ProcessOT_Gender()
        => ProcessEnumTag("OT Gender", pku.Game_Info.Gender, OT_Gender_Default, OT_Gender_Field.OT_Gender,
            OT_Gender_AlertIfUnspecified, GetOT_GenderAlert, x => x is Gender.Male or Gender.Female);

    public Alert GetOT_GenderAlert(AlertType at, string val, string defaultVal)
        => GetOT_GenderAlertBase(at, val, defaultVal);

    public Alert GetOT_GenderAlertBase(AlertType at, string val, string defaultVal)
        => GetEnumAlert("OT Gender", at, val, defaultVal);
}