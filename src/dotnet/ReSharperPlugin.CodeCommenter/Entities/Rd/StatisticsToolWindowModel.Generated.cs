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
  /// <p>Generated from: StatisticsToolWindowModel.kt:15</p>
  /// </summary>
  [JetBrains.Application.ShellComponent]
  public class StatisticsToolWindowModel : RdExtBase
  {
    //fields
    //public fields
    [NotNull] public IRdEndpoint<Unit, Unit> GetContent => _GetContent;
    [NotNull] public IRdCall<RdToolWindowContent, Unit> OnContentUpdated => _OnContentUpdated;
    [NotNull] public IRdCall<RdChangeNodeContext, Unit> OnNodeChanged => _OnNodeChanged;
    
    //private fields
    [NotNull] private readonly RdCall<Unit, Unit> _GetContent;
    [NotNull] private readonly RdCall<RdToolWindowContent, Unit> _OnContentUpdated;
    [NotNull] private readonly RdCall<RdChangeNodeContext, Unit> _OnNodeChanged;
    
    //primary constructor
    private StatisticsToolWindowModel(
      [NotNull] RdCall<Unit, Unit> getContent,
      [NotNull] RdCall<RdToolWindowContent, Unit> onContentUpdated,
      [NotNull] RdCall<RdChangeNodeContext, Unit> onNodeChanged
    )
    {
      if (getContent == null) throw new ArgumentNullException("getContent");
      if (onContentUpdated == null) throw new ArgumentNullException("onContentUpdated");
      if (onNodeChanged == null) throw new ArgumentNullException("onNodeChanged");
      
      _GetContent = getContent;
      _OnContentUpdated = onContentUpdated;
      _OnNodeChanged = onNodeChanged;
      BindableChildren.Add(new KeyValuePair<string, object>("getContent", _GetContent));
      BindableChildren.Add(new KeyValuePair<string, object>("onContentUpdated", _OnContentUpdated));
      BindableChildren.Add(new KeyValuePair<string, object>("onNodeChanged", _OnNodeChanged));
    }
    //secondary constructor
    private StatisticsToolWindowModel (
    ) : this (
      new RdCall<Unit, Unit>(JetBrains.Rd.Impl.Serializers.ReadVoid, JetBrains.Rd.Impl.Serializers.WriteVoid, JetBrains.Rd.Impl.Serializers.ReadVoid, JetBrains.Rd.Impl.Serializers.WriteVoid),
      new RdCall<RdToolWindowContent, Unit>(RdToolWindowContent.Read, RdToolWindowContent.Write, JetBrains.Rd.Impl.Serializers.ReadVoid, JetBrains.Rd.Impl.Serializers.WriteVoid),
      new RdCall<RdChangeNodeContext, Unit>(RdChangeNodeContext.Read, RdChangeNodeContext.Write, JetBrains.Rd.Impl.Serializers.ReadVoid, JetBrains.Rd.Impl.Serializers.WriteVoid)
    ) {}
    //deconstruct trait
    //statics
    
    
    
    protected override long SerializationHash => 2012028119564653557L;
    
    protected override Action<ISerializers> Register => RegisterDeclaredTypesSerializers;
    public static void RegisterDeclaredTypesSerializers(ISerializers serializers)
    {
      
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
        printer.Print("getContent = "); _GetContent.PrintEx(printer); printer.Println();
        printer.Print("onContentUpdated = "); _OnContentUpdated.PrintEx(printer); printer.Println();
        printer.Print("onNodeChanged = "); _OnNodeChanged.PrintEx(printer); printer.Println();
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
  /// <p>Generated from: StatisticsToolWindowModel.kt:43</p>
  /// </summary>
  public sealed class RdChangeNodeContext : IPrintable, IEquatable<RdChangeNodeContext>
  {
    //fields
    //public fields
    [NotNull] public RdRow NewNode {get; private set;}
    
    //private fields
    //primary constructor
    public RdChangeNodeContext(
      [NotNull] RdRow newNode
    )
    {
      if (newNode == null) throw new ArgumentNullException("newNode");
      
      NewNode = newNode;
    }
    //secondary constructor
    //deconstruct trait
    public void Deconstruct([NotNull] out RdRow newNode)
    {
      newNode = NewNode;
    }
    //statics
    
    public static CtxReadDelegate<RdChangeNodeContext> Read = (ctx, reader) => 
    {
      var newNode = RdRow.Read(ctx, reader);
      var _result = new RdChangeNodeContext(newNode);
      return _result;
    };
    
    public static CtxWriteDelegate<RdChangeNodeContext> Write = (ctx, writer, value) => 
    {
      RdRow.Write(ctx, writer, value.NewNode);
    };
    
    //constants
    
    //custom body
    //methods
    //equals trait
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((RdChangeNodeContext) obj);
    }
    public bool Equals(RdChangeNodeContext other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Equals(NewNode, other.NewNode);
    }
    //hash code trait
    public override int GetHashCode()
    {
      unchecked {
        var hash = 0;
        hash = hash * 31 + NewNode.GetHashCode();
        return hash;
      }
    }
    //pretty print
    public void Print(PrettyPrinter printer)
    {
      printer.Println("RdChangeNodeContext (");
      using (printer.IndentCookie()) {
        printer.Print("newNode = "); NewNode.PrintEx(printer); printer.Println();
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
  /// <p>Generated from: StatisticsToolWindowModel.kt:33</p>
  /// </summary>
  public enum RdLoadingState {
    Loading,
    Loaded,
    RelativeToChildren
  }
  
  
  /// <summary>
  /// <p>Generated from: StatisticsToolWindowModel.kt:16</p>
  /// </summary>
  public sealed class RdRow : IPrintable, IEquatable<RdRow>
  {
    //fields
    //public fields
    public RdRowType Type {get; private set;}
    [NotNull] public string Name {get; private set;}
    [CanBeNull] public string Docstring {get; private set;}
    public float Coverage {get; private set;}
    public float Quality {get; private set;}
    public RdLoadingState LoadingState {get; private set;}
    [NotNull] public List<RdRow> Children {get; private set;}
    
    //private fields
    //primary constructor
    public RdRow(
      RdRowType type,
      [NotNull] string name,
      [CanBeNull] string docstring,
      float coverage,
      float quality,
      RdLoadingState loadingState,
      [NotNull] List<RdRow> children
    )
    {
      if (name == null) throw new ArgumentNullException("name");
      if (children == null) throw new ArgumentNullException("children");
      
      Type = type;
      Name = name;
      Docstring = docstring;
      Coverage = coverage;
      Quality = quality;
      LoadingState = loadingState;
      Children = children;
    }
    //secondary constructor
    //deconstruct trait
    public void Deconstruct(out RdRowType type, [NotNull] out string name, [CanBeNull] out string docstring, out float coverage, out float quality, out RdLoadingState loadingState, [NotNull] out List<RdRow> children)
    {
      type = Type;
      name = Name;
      docstring = Docstring;
      coverage = Coverage;
      quality = Quality;
      loadingState = LoadingState;
      children = Children;
    }
    //statics
    
    public static CtxReadDelegate<RdRow> Read = (ctx, reader) => 
    {
      var type = (RdRowType)reader.ReadInt();
      var name = reader.ReadString();
      var docstring = ReadStringNullable(ctx, reader);
      var coverage = reader.ReadFloat();
      var quality = reader.ReadFloat();
      var loadingState = (RdLoadingState)reader.ReadInt();
      var children = ReadRdRowList(ctx, reader);
      var _result = new RdRow(type, name, docstring, coverage, quality, loadingState, children);
      return _result;
    };
    public static CtxReadDelegate<string> ReadStringNullable = JetBrains.Rd.Impl.Serializers.ReadString.NullableClass();
    public static CtxReadDelegate<List<RdRow>> ReadRdRowList = RdRow.Read.List();
    
    public static CtxWriteDelegate<RdRow> Write = (ctx, writer, value) => 
    {
      writer.Write((int)value.Type);
      writer.Write(value.Name);
      WriteStringNullable(ctx, writer, value.Docstring);
      writer.Write(value.Coverage);
      writer.Write(value.Quality);
      writer.Write((int)value.LoadingState);
      WriteRdRowList(ctx, writer, value.Children);
    };
    public static  CtxWriteDelegate<string> WriteStringNullable = JetBrains.Rd.Impl.Serializers.WriteString.NullableClass();
    public static  CtxWriteDelegate<List<RdRow>> WriteRdRowList = RdRow.Write.List();
    
    //constants
    
    //custom body
    //methods
    //equals trait
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((RdRow) obj);
    }
    public bool Equals(RdRow other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Type == other.Type && Name == other.Name && Equals(Docstring, other.Docstring) && Coverage == other.Coverage && Quality == other.Quality && LoadingState == other.LoadingState && Children.SequenceEqual(other.Children);
    }
    //hash code trait
    public override int GetHashCode()
    {
      unchecked {
        var hash = 0;
        hash = hash * 31 + (int) Type;
        hash = hash * 31 + Name.GetHashCode();
        hash = hash * 31 + (Docstring != null ? Docstring.GetHashCode() : 0);
        hash = hash * 31 + Coverage.GetHashCode();
        hash = hash * 31 + Quality.GetHashCode();
        hash = hash * 31 + (int) LoadingState;
        hash = hash * 31 + Children.ContentHashCode();
        return hash;
      }
    }
    //pretty print
    public void Print(PrettyPrinter printer)
    {
      printer.Println("RdRow (");
      using (printer.IndentCookie()) {
        printer.Print("type = "); Type.PrintEx(printer); printer.Println();
        printer.Print("name = "); Name.PrintEx(printer); printer.Println();
        printer.Print("docstring = "); Docstring.PrintEx(printer); printer.Println();
        printer.Print("coverage = "); Coverage.PrintEx(printer); printer.Println();
        printer.Print("quality = "); Quality.PrintEx(printer); printer.Println();
        printer.Print("loadingState = "); LoadingState.PrintEx(printer); printer.Println();
        printer.Print("children = "); Children.PrintEx(printer); printer.Println();
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
  /// <p>Generated from: StatisticsToolWindowModel.kt:26</p>
  /// </summary>
  public enum RdRowType {
    Module,
    File,
    Method,
    Root
  }
  
  
  /// <summary>
  /// <p>Generated from: StatisticsToolWindowModel.kt:39</p>
  /// </summary>
  public sealed class RdToolWindowContent : IPrintable, IEquatable<RdToolWindowContent>
  {
    //fields
    //public fields
    [NotNull] public List<RdRow> Rows {get; private set;}
    
    //private fields
    //primary constructor
    public RdToolWindowContent(
      [NotNull] List<RdRow> rows
    )
    {
      if (rows == null) throw new ArgumentNullException("rows");
      
      Rows = rows;
    }
    //secondary constructor
    //deconstruct trait
    public void Deconstruct([NotNull] out List<RdRow> rows)
    {
      rows = Rows;
    }
    //statics
    
    public static CtxReadDelegate<RdToolWindowContent> Read = (ctx, reader) => 
    {
      var rows = ReadRdRowList(ctx, reader);
      var _result = new RdToolWindowContent(rows);
      return _result;
    };
    public static CtxReadDelegate<List<RdRow>> ReadRdRowList = RdRow.Read.List();
    
    public static CtxWriteDelegate<RdToolWindowContent> Write = (ctx, writer, value) => 
    {
      WriteRdRowList(ctx, writer, value.Rows);
    };
    public static  CtxWriteDelegate<List<RdRow>> WriteRdRowList = RdRow.Write.List();
    
    //constants
    
    //custom body
    //methods
    //equals trait
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((RdToolWindowContent) obj);
    }
    public bool Equals(RdToolWindowContent other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Rows.SequenceEqual(other.Rows);
    }
    //hash code trait
    public override int GetHashCode()
    {
      unchecked {
        var hash = 0;
        hash = hash * 31 + Rows.ContentHashCode();
        return hash;
      }
    }
    //pretty print
    public void Print(PrettyPrinter printer)
    {
      printer.Println("RdToolWindowContent (");
      using (printer.IndentCookie()) {
        printer.Print("rows = "); Rows.PrintEx(printer); printer.Println();
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
