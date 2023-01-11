//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a RdGen v1.10.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

using JetBrains.Core;
using JetBrains.Diagnostics;
using JetBrains.Collections;
using JetBrains.Collections.Viewable;
using JetBrains.Lifetimes;
using JetBrains.Serialization;
using JetBrains.Rd;
using JetBrains.Rd.Base;
using JetBrains.Rd.Impl;
using JetBrains.Rd.Tasks;
using JetBrains.Rd.Util;
using JetBrains.Rd.Text;


// ReSharper disable RedundantEmptyObjectCreationArgumentList
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantOverflowCheckingContext


namespace JetBrains.Rider.Model
{
  
  
  /// <summary>
  /// <p>Generated from: StatisticsToolWindowModel.kt:18</p>
  /// </summary>
  [JetBrains.Application.ShellComponent]
  public class StatisticsToolWindowModel : RdExtBase
  {
    //fields
    //public fields
    [NotNull] public IViewableProperty<JetBrains.Rider.Model.UIAutomation.BeControl> ToolWindowContent => _ToolWindowContent;
    [NotNull] public IViewableProperty<bool> ActivateToolWindow => _ActivateToolWindow;
    
    //private fields
    [NotNull] private readonly RdProperty<JetBrains.Rider.Model.UIAutomation.BeControl> _ToolWindowContent;
    [NotNull] private readonly RdProperty<bool> _ActivateToolWindow;
    
    //primary constructor
    private StatisticsToolWindowModel(
      [NotNull] RdProperty<JetBrains.Rider.Model.UIAutomation.BeControl> toolWindowContent,
      [NotNull] RdProperty<bool> activateToolWindow
    )
    {
      if (toolWindowContent == null) throw new ArgumentNullException("toolWindowContent");
      if (activateToolWindow == null) throw new ArgumentNullException("activateToolWindow");
      
      _ToolWindowContent = toolWindowContent;
      _ActivateToolWindow = activateToolWindow;
      _ActivateToolWindow.OptimizeNested = true;
      BindableChildren.Add(new KeyValuePair<string, object>("toolWindowContent", _ToolWindowContent));
      BindableChildren.Add(new KeyValuePair<string, object>("activateToolWindow", _ActivateToolWindow));
    }
    //secondary constructor
    private StatisticsToolWindowModel (
    ) : this (
      new RdProperty<JetBrains.Rider.Model.UIAutomation.BeControl>(JetBrains.Rider.Model.UIAutomation.BeControl.Read, JetBrains.Rider.Model.UIAutomation.BeControl.Write),
      new RdProperty<bool>(JetBrains.Rd.Impl.Serializers.ReadBool, JetBrains.Rd.Impl.Serializers.WriteBool)
    ) {}
    //deconstruct trait
    //statics
    
    
    
    protected override long SerializationHash => -6019309499586322188L;
    
    protected override Action<ISerializers> Register => RegisterDeclaredTypesSerializers;
    public static void RegisterDeclaredTypesSerializers(ISerializers serializers)
    {
      serializers.Register(StatisticsToolWindowPanel.Read, StatisticsToolWindowPanel.Write);
      
      serializers.RegisterToplevelOnce(typeof(IdeRoot), IdeRoot.RegisterDeclaredTypesSerializers);
    }
    
    public StatisticsToolWindowModel(Lifetime lifetime, IProtocol protocol) : this()
    {
      Identify(protocol.Identities, RdId.Root.Mix("StatisticsToolWindowModel"));
      Bind(lifetime, protocol, "StatisticsToolWindowModel");
    }
    
    //constants
    
    //custom body
    //methods
    //equals trait
    //hash code trait
    //pretty print
    public override void Print(PrettyPrinter printer)
    {
      printer.Println("StatisticsToolWindowModel (");
      using (printer.IndentCookie()) {
        printer.Print("toolWindowContent = "); _ToolWindowContent.PrintEx(printer); printer.Println();
        printer.Print("activateToolWindow = "); _ActivateToolWindow.PrintEx(printer); printer.Println();
      }
      printer.Print(")");
    }
    //toString
    public override string ToString()
    {
      var printer = new SingleLinePrettyPrinter();
      Print(printer);
      return printer.ToString();
    }
  }
  
  
  /// <summary>
  /// <p>Generated from: StatisticsToolWindowModel.kt:24</p>
  /// </summary>
  public sealed class StatisticsToolWindowPanel : JetBrains.Rider.Model.UIAutomation.BeControl
  {
    //fields
    //public fields
    [CanBeNull] public string Url {get; private set;}
    [CanBeNull] public string Html {get; private set;}
    [NotNull] public ISignal<bool> OpenDevTools => _OpenDevTools;
    [NotNull] public ISignal<string> OpenUrl => _OpenUrl;
    [NotNull] public IRdEndpoint<string, string> GetResource => _GetResource;
    [NotNull] public IRdCall<string, Unit> SendMessage => _SendMessage;
    [NotNull] public ISignal<string> MessageReceived => _MessageReceived;
    
    //private fields
    [NotNull] private readonly RdSignal<bool> _OpenDevTools;
    [NotNull] private readonly RdSignal<string> _OpenUrl;
    [NotNull] private readonly RdCall<string, string> _GetResource;
    [NotNull] private readonly RdCall<string, Unit> _SendMessage;
    [NotNull] private readonly RdSignal<string> _MessageReceived;
    
    //primary constructor
    private StatisticsToolWindowPanel(
      [CanBeNull] string url,
      [CanBeNull] string html,
      [NotNull] RdSignal<bool> openDevTools,
      [NotNull] RdSignal<string> openUrl,
      [NotNull] RdCall<string, string> getResource,
      [NotNull] RdCall<string, Unit> sendMessage,
      [NotNull] RdSignal<string> messageReceived,
      [NotNull] RdProperty<bool> enabled,
      [NotNull] RdProperty<string> controlId,
      [NotNull] RdProperty<string> tooltip,
      [NotNull] RdSignal<Unit> focus,
      [NotNull] RdProperty<JetBrains.Rider.Model.UIAutomation.ControlVisibility> visible
    ) : base (
      enabled,
      controlId,
      tooltip,
      focus,
      visible
     ) 
    {
      if (openDevTools == null) throw new ArgumentNullException("openDevTools");
      if (openUrl == null) throw new ArgumentNullException("openUrl");
      if (getResource == null) throw new ArgumentNullException("getResource");
      if (sendMessage == null) throw new ArgumentNullException("sendMessage");
      if (messageReceived == null) throw new ArgumentNullException("messageReceived");
      
      Url = url;
      Html = html;
      _OpenDevTools = openDevTools;
      _OpenUrl = openUrl;
      _GetResource = getResource;
      _SendMessage = sendMessage;
      _MessageReceived = messageReceived;
      _GetResource.Async = true;
      _SendMessage.Async = true;
      _MessageReceived.Async = true;
      BindableChildren.Add(new KeyValuePair<string, object>("openDevTools", _OpenDevTools));
      BindableChildren.Add(new KeyValuePair<string, object>("openUrl", _OpenUrl));
      BindableChildren.Add(new KeyValuePair<string, object>("getResource", _GetResource));
      BindableChildren.Add(new KeyValuePair<string, object>("sendMessage", _SendMessage));
      BindableChildren.Add(new KeyValuePair<string, object>("messageReceived", _MessageReceived));
    }
    //secondary constructor
    public StatisticsToolWindowPanel (
      [CanBeNull] string url,
      [CanBeNull] string html
    ) : this (
      url,
      html,
      new RdSignal<bool>(JetBrains.Rd.Impl.Serializers.ReadBool, JetBrains.Rd.Impl.Serializers.WriteBool),
      new RdSignal<string>(JetBrains.Rd.Impl.Serializers.ReadString, JetBrains.Rd.Impl.Serializers.WriteString),
      new RdCall<string, string>(JetBrains.Rd.Impl.Serializers.ReadString, JetBrains.Rd.Impl.Serializers.WriteString, JetBrains.Rd.Impl.Serializers.ReadString, JetBrains.Rd.Impl.Serializers.WriteString),
      new RdCall<string, Unit>(JetBrains.Rd.Impl.Serializers.ReadString, JetBrains.Rd.Impl.Serializers.WriteString, JetBrains.Rd.Impl.Serializers.ReadVoid, JetBrains.Rd.Impl.Serializers.WriteVoid),
      new RdSignal<string>(JetBrains.Rd.Impl.Serializers.ReadString, JetBrains.Rd.Impl.Serializers.WriteString),
      new RdProperty<bool>(JetBrains.Rd.Impl.Serializers.ReadBool, JetBrains.Rd.Impl.Serializers.WriteBool, true),
      new RdProperty<string>(JetBrains.Rd.Impl.Serializers.ReadString, JetBrains.Rd.Impl.Serializers.WriteString, ""),
      new RdProperty<string>(ReadStringNullable, WriteStringNullable),
      new RdSignal<Unit>(JetBrains.Rd.Impl.Serializers.ReadVoid, JetBrains.Rd.Impl.Serializers.WriteVoid),
      new RdProperty<JetBrains.Rider.Model.UIAutomation.ControlVisibility>(ReadControlVisibility, WriteControlVisibility)
    ) {}
    //deconstruct trait
    //statics
    
    public static new CtxReadDelegate<StatisticsToolWindowPanel> Read = (ctx, reader) => 
    {
      var _id = RdId.Read(reader);
      var enabled = RdProperty<bool>.Read(ctx, reader, JetBrains.Rd.Impl.Serializers.ReadBool, JetBrains.Rd.Impl.Serializers.WriteBool);
      var controlId = RdProperty<string>.Read(ctx, reader, JetBrains.Rd.Impl.Serializers.ReadString, JetBrains.Rd.Impl.Serializers.WriteString);
      var tooltip = RdProperty<string>.Read(ctx, reader, ReadStringNullable, WriteStringNullable);
      var focus = RdSignal<Unit>.Read(ctx, reader, JetBrains.Rd.Impl.Serializers.ReadVoid, JetBrains.Rd.Impl.Serializers.WriteVoid);
      var visible = RdProperty<JetBrains.Rider.Model.UIAutomation.ControlVisibility>.Read(ctx, reader, ReadControlVisibility, WriteControlVisibility);
      var url = ReadStringNullable(ctx, reader);
      var html = ReadStringNullable(ctx, reader);
      var openDevTools = RdSignal<bool>.Read(ctx, reader, JetBrains.Rd.Impl.Serializers.ReadBool, JetBrains.Rd.Impl.Serializers.WriteBool);
      var openUrl = RdSignal<string>.Read(ctx, reader, JetBrains.Rd.Impl.Serializers.ReadString, JetBrains.Rd.Impl.Serializers.WriteString);
      var getResource = RdCall<string, string>.Read(ctx, reader, JetBrains.Rd.Impl.Serializers.ReadString, JetBrains.Rd.Impl.Serializers.WriteString, JetBrains.Rd.Impl.Serializers.ReadString, JetBrains.Rd.Impl.Serializers.WriteString);
      var sendMessage = RdCall<string, Unit>.Read(ctx, reader, JetBrains.Rd.Impl.Serializers.ReadString, JetBrains.Rd.Impl.Serializers.WriteString, JetBrains.Rd.Impl.Serializers.ReadVoid, JetBrains.Rd.Impl.Serializers.WriteVoid);
      var messageReceived = RdSignal<string>.Read(ctx, reader, JetBrains.Rd.Impl.Serializers.ReadString, JetBrains.Rd.Impl.Serializers.WriteString);
      var _result = new StatisticsToolWindowPanel(url, html, openDevTools, openUrl, getResource, sendMessage, messageReceived, enabled, controlId, tooltip, focus, visible).WithId(_id);
      return _result;
    };
    public static CtxReadDelegate<string> ReadStringNullable = JetBrains.Rd.Impl.Serializers.ReadString.NullableClass();
    public static CtxReadDelegate<JetBrains.Rider.Model.UIAutomation.ControlVisibility> ReadControlVisibility = new CtxReadDelegate<JetBrains.Rider.Model.UIAutomation.ControlVisibility>(JetBrains.Rd.Impl.Serializers.ReadEnum<JetBrains.Rider.Model.UIAutomation.ControlVisibility>);
    
    public static new CtxWriteDelegate<StatisticsToolWindowPanel> Write = (ctx, writer, value) => 
    {
      value.RdId.Write(writer);
      RdProperty<bool>.Write(ctx, writer, value._Enabled);
      RdProperty<string>.Write(ctx, writer, value._ControlId);
      RdProperty<string>.Write(ctx, writer, value._Tooltip);
      RdSignal<Unit>.Write(ctx, writer, value._Focus);
      RdProperty<JetBrains.Rider.Model.UIAutomation.ControlVisibility>.Write(ctx, writer, value._Visible);
      WriteStringNullable(ctx, writer, value.Url);
      WriteStringNullable(ctx, writer, value.Html);
      RdSignal<bool>.Write(ctx, writer, value._OpenDevTools);
      RdSignal<string>.Write(ctx, writer, value._OpenUrl);
      RdCall<string, string>.Write(ctx, writer, value._GetResource);
      RdCall<string, Unit>.Write(ctx, writer, value._SendMessage);
      RdSignal<string>.Write(ctx, writer, value._MessageReceived);
    };
    public static  CtxWriteDelegate<string> WriteStringNullable = JetBrains.Rd.Impl.Serializers.WriteString.NullableClass();
    public static  CtxWriteDelegate<JetBrains.Rider.Model.UIAutomation.ControlVisibility> WriteControlVisibility = new CtxWriteDelegate<JetBrains.Rider.Model.UIAutomation.ControlVisibility>(JetBrains.Rd.Impl.Serializers.WriteEnum<JetBrains.Rider.Model.UIAutomation.ControlVisibility>);
    
    //constants
    
    //custom body
    //methods
    //equals trait
    //hash code trait
    //pretty print
    public override void Print(PrettyPrinter printer)
    {
      printer.Println("StatisticsToolWindowPanel (");
      using (printer.IndentCookie()) {
        printer.Print("url = "); Url.PrintEx(printer); printer.Println();
        printer.Print("html = "); Html.PrintEx(printer); printer.Println();
        printer.Print("openDevTools = "); _OpenDevTools.PrintEx(printer); printer.Println();
        printer.Print("openUrl = "); _OpenUrl.PrintEx(printer); printer.Println();
        printer.Print("getResource = "); _GetResource.PrintEx(printer); printer.Println();
        printer.Print("sendMessage = "); _SendMessage.PrintEx(printer); printer.Println();
        printer.Print("messageReceived = "); _MessageReceived.PrintEx(printer); printer.Println();
        printer.Print("enabled = "); _Enabled.PrintEx(printer); printer.Println();
        printer.Print("controlId = "); _ControlId.PrintEx(printer); printer.Println();
        printer.Print("tooltip = "); _Tooltip.PrintEx(printer); printer.Println();
        printer.Print("focus = "); _Focus.PrintEx(printer); printer.Println();
        printer.Print("visible = "); _Visible.PrintEx(printer); printer.Println();
      }
      printer.Print(")");
    }
    //toString
    public override string ToString()
    {
      var printer = new SingleLinePrettyPrinter();
      Print(printer);
      return printer.ToString();
    }
  }
}
