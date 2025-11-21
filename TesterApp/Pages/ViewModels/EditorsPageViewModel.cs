using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Cobalt.Avalonia.Desktop;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Cryptography.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace TesterApp.Pages.ViewModels;

public class EditorsPageViewModel : ObservableValidator
{
    public EditorsPageViewModel()
    {
        GenerateRandomDataCommand = new AsyncRelayCommand(GenerateRandomData);
        ValidateViewModelCommand = new AsyncRelayCommand(ValidateViewModel);
    }
    
    public IServiceProvider ServiceProvider => Program.Bootstrapper.ServiceProvider;
    public IFilePickerService FilePickerService => ServiceProvider.GetRequiredService<IFilePickerService>();
    public IFolderPickerService FolderPickerService => ServiceProvider.GetRequiredService<IFolderPickerService>();

    [Required]
    [MinLength(10)]
    public string? StringValueShort
    {
        get => _stringValueShort;
        set => SetProperty(ref _stringValueShort, value, true);
    }
    private string? _stringValueShort;
    
    public string? StringValueLong
    {
        get => _stringValueLong;
        set => SetProperty(ref _stringValueLong, value);
    }
    private string? _stringValueLong;
    
    public double DoubleValue
    {
        get => _doubleValue;
        set
        {
            SetProperty(ref _doubleValue, value);  
            Console.WriteLine($"Value: {value}");
        }
    }
    private double _doubleValue = 3.1;
    
    public float? FloatValue
    {
        get => _floatValue;
        set => SetProperty(ref _floatValue, value);
    }
    private float? _floatValue;

    public decimal? DecimalValue
    {
        get => _decimalValue;
        set => SetProperty(ref _decimalValue, value);
    }
    private decimal? _decimalValue;

    public short? ShortValue
    {
        get => _shortValue;
        set => SetProperty(ref _shortValue, value);
    }
    private short? _shortValue;
    
    public ushort? UShortValue
    {
        get => _ushortValue;
        set => SetProperty(ref _ushortValue, value);
    }
    private ushort? _ushortValue;

    [Required(ErrorMessage = "Missing int value")]
    [Range(0, 100, ErrorMessage = "Incorrect value! Value must be between 0 and 100")]
    public int? IntValue
    {
        get => _intValue;
        set => SetProperty(ref _intValue, value, true);
    }
    private int? _intValue;

    public uint? UIntValue
    {
        get => _uintValue;
        set => SetProperty(ref _uintValue, value);
    }
    private uint? _uintValue;
    
    public long? LongValue
    {
        get => _longValue;
        set => SetProperty(ref _longValue, value);
    }
    private long? _longValue;
    
    public ulong? ULongValue
    {
        get => _ulongValue;
        set => SetProperty(ref _ulongValue, value);
    }
    private ulong? _ulongValue;
    
    public byte[]? DataValue
    {
        get => _dataValue;
        set => SetProperty(ref _dataValue, value);
    }
    private byte[]? _dataValue;

    public string? FileValue
    {
        get => _fileValue;
        set => SetProperty(ref _fileValue, value);
    }
    private string? _fileValue;

    public string? FolderValue
    {
        get => _folderValue;
        set => SetProperty(ref _folderValue, value);
    }
    private string? _folderValue;
    
    public AsyncRelayCommand GenerateRandomDataCommand { get; }
    public AsyncRelayCommand ValidateViewModelCommand { get; }

    private async Task GenerateRandomData()
    {
        DataValue = RandomUtils.GenerateRandomBytes(16);
        await Task.CompletedTask;
    }

    private async Task ValidateViewModel()
    {
        ValidateAllProperties();
        await Task.CompletedTask;
    }
}