using System;
namespace SharpIpp.Protocol.Models;

/// <summary>
/// Identifies the device output bin to which the job is to be delivered.
/// There are standard values whose attribute syntax is 'keyword', but there are no standard values
/// whose attribute syntax is 'name'. Output bins whose attribute syntax is 'name', if any, are
/// assigned by local administrators.
/// <para>
/// Use <see cref="IsMarked"/> to distinguish keyword values (standard) from name values (locally assigned):
/// <c>IsMarked = true</c> → keyword; <c>IsMarked = false</c> → nameWithoutLanguage.
/// </para>
/// See: PWG 5100.2-2001 Section 2.1
/// </summary>
public readonly record struct OutputBin(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum 
{
    /// <summary>
    /// The output-bin that, when facing the device, is best identified as the "top" bin
    /// with respect to the device.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public static readonly OutputBin Top = new("top");

    /// <summary>
    /// The output-bin that, when facing the device, is best identified as the "bottom" bin
    /// with respect to the device.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public static readonly OutputBin Bottom = new("bottom");

    /// <summary>
    /// The output-bin that, when facing the device, is best identified as the "middle" bin
    /// with respect to the device.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public static readonly OutputBin Middle = new("middle");

    /// <summary>
    /// The output-bin that, when facing the device, is best identified as the "rear" bin
    /// with respect to the device.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public static readonly OutputBin Rear = new("rear");

    /// <summary>
    /// The output-bin that, when facing the device, is best identified as the "side" bin
    /// with respect to the device.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public static readonly OutputBin Side = new("side");

    /// <summary>
    /// The output-bin that, when facing the device, is best identified as the "left" bin
    /// with respect to the device.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public static readonly OutputBin Left = new("left");

    /// <summary>
    /// The output-bin that, when facing the device, is best identified as the "right" bin
    /// with respect to the device.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public static readonly OutputBin Right = new("right");

    /// <summary>
    /// The output-bin that, when facing the device, is best identified as the "center" bin
    /// with respect to the device.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public static readonly OutputBin Center = new("center");

    /// <summary>
    /// The output-bin that is best identified as the "face-up" bin with respect to the device.
    /// The selection of this output bin does not cause output to be made face-up; rather this
    /// output bin is given this name because a sheet with printing on one side arrives in the
    /// output bin in the face-up position.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public static readonly OutputBin FaceUp = new("face-up");

    /// <summary>
    /// The output-bin that is best identified as the "face-down" bin with respect to the device.
    /// The selection of this output bin does not cause output to be made face-down; rather this
    /// output bin is given this name because a sheet with printing on one side arrives in the
    /// output bin in the face-down position.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public static readonly OutputBin FaceDown = new("face-down");

    /// <summary>
    /// The output-bin that is best identified as the "large-capacity" bin (in terms of the number
    /// of sheets) with respect to the device.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public static readonly OutputBin LargeCapacity = new("large-capacity");

    /// <summary>
    /// The output-bin that is best identified as functioning like a private "mailbox" with respect
    /// to the device. An output-bin functions like a private mailbox if a printer selects the actual
    /// output bin using additional implementation-dependent criteria, such as the authenticated user,
    /// that depends on the user submitting the job.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public static readonly OutputBin MyMailbox = new("my-mailbox");

    /// <summary>
    /// The first stacker output-bin ('stacker-1'). A stacker is typically used to collate sheets
    /// within a single document. If the 'stacker-N' group is supported, at least 'stacker-1' MUST
    /// be supported. Use <see cref="Stacker(int)"/> to create values for other stacker numbers.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public static readonly OutputBin Stacker1 = new("stacker-1");

    /// <summary>
    /// The first mailbox output-bin ('mailbox-1'). Each mailbox is typically used to collect jobs
    /// for an individual or group. If the 'mailbox-N' group is supported, at least 'mailbox-1' MUST
    /// be supported. Use <see cref="Mailbox(int)"/> to create values for other mailbox numbers.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public static readonly OutputBin Mailbox1 = new("mailbox-1");

    /// <summary>
    /// The first tray output-bin ('tray-1'). Output bins that are best identified as 'tray-N'
    /// rather than the descriptive names defined in the keyword list.
    /// Use <see cref="Tray(int)"/> to create values for other tray numbers.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public static readonly OutputBin Tray1 = new("tray-1");

    /// <summary>
    /// Automatic output-bin selection by the device.
    /// <para>
    /// Note: This value is not defined in PWG 5100.2-2001; it is a common vendor and CUPS extension.
    /// </para>
    /// </summary>
    public static readonly OutputBin Auto = new("auto");

    /// <summary>
    /// Creates a 'stacker-N' keyword value for the specified stacker number.
    /// The spec recommends representing 'stacker-1' to 'stacker-10' for interoperability.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    /// <param name="number">The stacker number (must be greater than 0).</param>
    public static OutputBin Stacker(int number) => new($"stacker-{ValidatePositive(number, nameof(number))}");

    /// <summary>
    /// Creates a 'mailbox-N' keyword value for the specified mailbox number.
    /// The spec recommends representing 'mailbox-1' to 'mailbox-25' for interoperability.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    /// <param name="number">The mailbox number (must be greater than 0).</param>
    public static OutputBin Mailbox(int number) => new($"mailbox-{ValidatePositive(number, nameof(number))}");

    /// <summary>
    /// Creates a 'tray-N' keyword value for the specified tray number.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    /// <param name="number">The tray number (must be greater than 0).</param>
    public static OutputBin Tray(int number) => new($"tray-{ValidatePositive(number, nameof(number))}");

    public override string ToString() => Value;
    public static implicit operator string(OutputBin bin) => bin.Value;
    public static explicit operator OutputBin(string value) => new(value);

    private static int ValidatePositive(int value, string parameterName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(parameterName, "Value must be greater than 0.");

        return value;
    }
}
