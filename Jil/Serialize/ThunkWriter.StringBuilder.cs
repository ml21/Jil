﻿#if !BUFFER_AND_SEQUENCE
using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Jil.Serialize
{
    internal ref partial struct ThunkWriter
    {
        public bool HasValue => Builder != null;

        TextWriter Builder;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Init(TextWriter writer)
        {
            Builder = writer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(float f)
        {
            Write(f.ToString(CultureInfo.InvariantCulture));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(double d)
        {
            Write(d.ToString(CultureInfo.InvariantCulture));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(decimal m)
        {
            Write(m.ToString(CultureInfo.InvariantCulture));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(char[] ch, int startIx, int len)
        {
            Builder.Write(ch, startIx, len);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(char ch)
        {
            Builder.Write(ch);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteCommonConstant(ConstantString_Common str)
        {
            var asUShort = (ushort)str;
            var ix = asUShort >> 8;
            var size = asUShort & 0xFF;

            Builder.Write(ThunkWriterCharArrays.ConstantString_Common_Chars, ix, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteFormattingConstant(ConstantString_Formatting str)
        {
            var asUShort = (ushort)str;
            var ix = (asUShort >> 8);
            var len = asUShort & 0xFF;

            Builder.Write(ThunkWriterCharArrays.ConstantString_Formatting_Chars, ix, len);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteMinConstant(ConstantString_Min str)
        {
            var asUShort = (ushort)str;
            var ix = (asUShort >> 8);
            var len = asUShort & 0xFF;

            Builder.Write(ThunkWriterCharArrays.ConstantString_Min_Chars, ix, len);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteValueConstant(ConstantString_Value str)
        {
            var asUShort = (ushort)str;
            var ix = (asUShort >> 8);
            var len = asUShort & 0xFF;

            Builder.Write(ThunkWriterCharArrays.ConstantString_Value_Chars, ix, len);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write000EscapeConstant(ConstantString_000Escape str)
        {
            var ix = (byte)str;

            Builder.Write(ThunkWriterCharArrays.Escape000Prefix);
            Builder.Write(ThunkWriterCharArrays.ConstantString_000Escape_Chars[ix]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write001EscapeConstant(ConstantString_001Escape str)
        {
            var ix = (byte)str;

            Builder.Write(ThunkWriterCharArrays.Escape001Prefix);
            Builder.Write(ThunkWriterCharArrays.ConstantString_001Escape_Chars[ix]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteDayOfWeek(ConstantString_DaysOfWeek str)
        {
            var ix = (byte)str;
            Builder.Write(ThunkWriterCharArrays.ConstantString_DaysOfWeek, ix, 3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Write(string strRef)
        {
            Builder.Write(strRef);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void End() { }
    }
}
#endif