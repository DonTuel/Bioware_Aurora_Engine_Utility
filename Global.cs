using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using CustomExtensions;

namespace Bioware_Aurora_Engine_Utility
{
    public static class Global
    {
        #region "Local Variables"
        public const Int32 O_RDONLY = 0x0000;   /* open for reading only */
        public const Int32 O_WRONLY = 0x0001;   /* open for writing only */
        public const Int32 O_RDWR = 0x0002;     /* open for reading and writing */
        public const Int32 O_APPEND = 0x0008;   /* writes done at eof */
        /* sequential/random access hints */
        public const Int32 O_RANDOM = 0x0010;  /* file access is primarily random */
        public const Int32 O_SEQUENTIAL = 0x0020;  /* file access is primarily sequential */
        /* Temporary file bit - file is deleted when last handle is closed */
        public const Int32 O_TEMPORARY = 0x0040;  /* temporary file bit */
        /* Open handle inherit bit */
        public const Int32 O_NOINHERIT = 0x0080;  /* child process doesn't inherit file */
        public const Int32 O_CREAT = 0x0100;    /* create and open file */
        public const Int32 O_TRUNC = 0x0200;    /* open and truncate */
        public const Int32 O_EXCL = 0x0400;     /* open only if file doesn't already exist */
        /* O_TEXT files have <cr><lf> sequences translated to <lf> on read()'s,
        ** and <lf> sequences translated to <cr><lf> on write()'s
        */
        /* temporary access hint */
        public const Int32 O_SHORT_LIVED = 0x1000;  /* temporary storage file, try not to flush */
        /* directory access hint */
        public const Int32 O_OBTAIN_DIR = 0x2000;  /* get information about a directory */
        public const Int32 O_TEXT = 0x4000;     /* file mode is text (translated) */
        public const Int32 O_BINARY = 0x8000;   /* file mode is binary (untranslated) */
        /* macro to translate the C 2.0 name used to force binary mode for files */
        public const Int32 O_RAW = 0x8000;
        public const Int32 O_WTEXT = 0x10000;   /* file mode is UTF16 (translated) */
        public const Int32 O_U16TEXT = 0x20000; /* file mode is UTF16 no BOM (translated) */
        public const Int32 O_U8TEXT = 0x40000;  /* file mode is UTF8  no BOM (translated) */

        public const Int16 IMAGE_OPEN_DASDCOPY = 0x01;
        public const Int16 IMAGE_OPEN_QUIET = 0x02;
        public const Int16 IMAGE_OPEN_DVOL1 = 0x04;

        public const Int16 DASD_CKDDEV = 1;       /* Lookup CKD device         */
        public const Int16 DASD_CKDCU = 2;        /* Lookup CKD control unit   */
        public const Int16 DASD_FBADEV = 3;       /* Lookup FBA device         */
        public const Int16 DASD_STDBLK = 4;       /* Lookup device standard block/physical */

        public const UInt16 DEFAULT_FBA_TYPE = 0x3370;

        public static UInt16 nextnum = 0;

        public static Boolean HexDumpOnly = false;

        public static Stream sReader;

        public static RichTextBox diag = null;
        public static Boolean genDiag = false;
        public static Byte[] eighthexff = { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

        public static Byte[]
        ascii_to_ebcdic = {
         /*         x0    x1    x2    x3    x4    x5    x6    x7    x8    x9    xA    xB    xC    xD    xE    xF */
         /* 0x */ 0x00, 0x01, 0x02, 0x03, 0x37, 0x2D, 0x2E, 0x2F, 0x16, 0x05, 0x25, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
         /* 1x */ 0x10, 0x11, 0x12, 0x13, 0x3C, 0x3D, 0x32, 0x26, 0x18, 0x19, 0x1A, 0x27, 0x22, 0x1D, 0x35, 0x1F,
         /* 2x */ 0x40, 0x5A, 0x7F, 0x7B, 0x5B, 0x6C, 0x50, 0x7D, 0x4D, 0x5D, 0x5C, 0x4E, 0x6B, 0x60, 0x4B, 0x61,
         /* 3x */ 0xF0, 0xF1, 0xF2, 0xF3, 0xF4, 0xF5, 0xF6, 0xF7, 0xF8, 0xF9, 0x7A, 0x5E, 0x4C, 0x7E, 0x6E, 0x6F,
         /* 4x */ 0x7C, 0xC1, 0xC2, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0xC8, 0xC9, 0xD1, 0xD2, 0xD3, 0xD4, 0xD5, 0xD6,
         /* 5x */ 0xD7, 0xD8, 0xD9, 0xE2, 0xE3, 0xE4, 0xE5, 0xE6, 0xE7, 0xE8, 0xE9, 0xAD, 0xE0, 0xBD, 0x5F, 0x6D,
         /* 6x */ 0x79, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96,
         /* 7x */ 0x97, 0x98, 0x99, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xC0, 0x6A, 0xD0, 0xA1, 0x07,
         /* 8x */ 0x68, 0xDC, 0x51, 0x42, 0x43, 0x44, 0x47, 0x48, 0x52, 0x53, 0x54, 0x57, 0x56, 0x58, 0x63, 0x67,
         /* 9x */ 0x71, 0x9C, 0x9E, 0xCB, 0xCC, 0xCD, 0xDB, 0xDD, 0xDF, 0xEC, 0xFC, 0xB0, 0xB1, 0xB2, 0xB3, 0xB4,
         /* Ax */ 0x45, 0x55, 0xCE, 0xDE, 0x49, 0x69, 0x04, 0x06, 0xAB, 0x08, 0xBA, 0xB8, 0xB7, 0xAA, 0x8A, 0x8B,
         /* Bx */ 0x09, 0x0A, 0x14, 0xBB, 0x15, 0xB5, 0xB6, 0x17, 0x1B, 0xB9, 0x1C, 0x1E, 0xBC, 0x20, 0xBE, 0xBF,
         /* Cx */ 0x21, 0x23, 0x24, 0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x30, 0x31, 0xCA, 0x33, 0x34, 0x36, 0x38, 0xCF,
         /* Dx */ 0x39, 0x3A, 0x3B, 0x3E, 0x41, 0x46, 0x4A, 0x4F, 0x59, 0x62, 0xDA, 0x64, 0x65, 0x66, 0x70, 0x72,
         /* Ex */ 0x73, 0xE1, 0x74, 0x75, 0x76, 0x77, 0x78, 0x80, 0x8C, 0x8D, 0x8E, 0xEB, 0x8F, 0xED, 0xEE, 0xEF,
         /* Fx */ 0x90, 0x9A, 0x9B, 0x9D, 0x9F, 0xA0, 0xAC, 0xAE, 0xAF, 0xFD, 0xFE, 0xFB, 0x3F, 0xEA, 0xFA, 0xFF
         };     /*  x0    x1    x2    x3    x4    x5    x6    x7    x8    x9    xA    xB    xC    xD    xE    xF */

        public static Byte[]
        ebcdic_to_ascii = {
         /*         x0    x1    x2    x3    x4    x5    x6    x7    x8    x9    xA    xB    xC    xD    xE    xF */
         /* 0x */ 0x00, 0x01, 0x02, 0x03, 0xA6, 0x09, 0xA7, 0x7F, 0xA9, 0xB0, 0xB1, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
         /* 1x */ 0x10, 0x11, 0x12, 0x13, 0xB2, 0x0A, 0x08, 0xB7, 0x18, 0x19, 0x1A, 0xB8, 0xBA, 0x1D, 0xBB, 0x1F,
         /* 2x */ 0xBD, 0xC0, 0x1C, 0xC1, 0xC2, 0x0A, 0x17, 0x1B, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0x05, 0x06, 0x07,
         /* 3x */ 0xC8, 0xC9, 0x16, 0xCB, 0xCC, 0x1E, 0xCD, 0x04, 0xCE, 0xD0, 0xD1, 0xD2, 0x14, 0x15, 0xD3, 0xFC,
         /* 4x */ 0x20, 0xD4, 0x83, 0x84, 0x85, 0xA0, 0xD5, 0x86, 0x87, 0xA4, 0xD6, 0x2E, 0x3C, 0x28, 0x2B, 0xD7,
         /* 5x */ 0x26, 0x82, 0x88, 0x89, 0x8A, 0xA1, 0x8C, 0x8B, 0x8D, 0xD8, 0x21, 0x24, 0x2A, 0x29, 0x3B, 0x5E,
         /* 6x */ 0x2D, 0x2F, 0xD9, 0x8E, 0xDB, 0xDC, 0xDD, 0x8F, 0x80, 0xA5, 0x7C, 0x2C, 0x25, 0x5F, 0x3E, 0x3F,
         /* 7x */ 0xDE, 0x90, 0xDF, 0xE0, 0xE2, 0xE3, 0xE4, 0xE5, 0xE6, 0x60, 0x3A, 0x23, 0x40, 0x27, 0x3D, 0x22,
         /* 8x */ 0xE7, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0xAE, 0xAF, 0xE8, 0xE9, 0xEA, 0xEC,
         /* 9x */ 0xF0, 0x6A, 0x6B, 0x6C, 0x6D, 0x6E, 0x6F, 0x70, 0x71, 0x72, 0xF1, 0xF2, 0x91, 0xF3, 0x92, 0xF4,
         /* Ax */ 0xF5, 0x7E, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7A, 0xAD, 0xA8, 0xF6, 0x5B, 0xF7, 0xF8,
         /* Bx */ 0x9B, 0x9C, 0x9D, 0x9E, 0x9F, 0xB5, 0xB6, 0xAC, 0xAB, 0xB9, 0xAA, 0xB3, 0xBC, 0x5D, 0xBE, 0xBF,
         /* Cx */ 0x7B, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0xCA, 0x93, 0x94, 0x95, 0xA2, 0xCF,
         /* Dx */ 0x7D, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F, 0x50, 0x51, 0x52, 0xDA, 0x96, 0x81, 0x97, 0xA3, 0x98,
         /* Ex */ 0x5C, 0xE1, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5A, 0xFD, 0xEB, 0x99, 0xED, 0xEE, 0xEF,
         /* Fx */ 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0xFE, 0xFB, 0x9A, 0xF9, 0xFA, 0xFF
         };     /*  x0    x1    x2    x3    x4    x5    x6    x7    x8    x9    xA    xB    xC    xD    xE    xF */
        #endregion

        public static String[,] extensions = new string[,] { { "masm", "Mainframe Assembler" }, { "jcl", "Mainframe JCL" },
            { "cob", "Cobol" } , { "rexx", "REXX Procedural Language" }, {"cntl", "Control statements"}, {"txt", "Text" } };

        public static String _rtfHdr = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Consolas;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs22 ";
        public static String _rtfEnd = "}\r\n";
        public static String _rtfNL = "\\par\r\n";

        public static String filter = "";
        public static String folder = "";
        private static readonly Int32 _curYY = DateTime.Today.Year % 100;

        public static int _columnWidth = 32;

        public enum SaveResults
        {
            Fail = 0,
            Success = 1,
            Cancel = 2
        }

        public static String JulDate(BINYDD date)
        {
            Int32 curYr = _curYY;
            String ddd = ((UInt16)date.DD.big_endian).ToString("000");
            ddd = ddd.Substring(ddd.Length - 3);
            if (date.Y <= curYr)
            {
                curYr = date.Y + 2000;
            }
            else
            {
                curYr = date.Y + 1900;
            }
            return curYr.ToString() + "." + ddd;
        }

        public static String YYDDD(BINYDD date)
        {
            String ddd = ((UInt16)date.DD.big_endian).ToString("000");
            ddd = ddd.Substring(ddd.Length - 3);
            return date.Y.ToString("00") + "." + ddd;
        }

        #region Assembly Attribute Accessors

        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                string[] version = Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
                return version[0] + "." + version[1];
            }
        }

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
    }

    public class extObject : Object
    {
        public void Debug(String fieldName)
        {
            if (!Global.genDiag) return;
            if (Global.diag == null) return;

            StackTrace st = new StackTrace(new StackFrame(1, true));
            StackTrace st2 = new StackTrace();
            StackFrame sf = st.GetFrame(0);
            MethodBase method = sf.GetMethod();

            String clsName = this.GetType().Name;

            switch (clsName)
            {
                case "DEVBLK":
                    Debug_DEVBLK(fieldName, this, method);
                    break;

                case "HWORD":
                case "FWORD":
                case "DBLWRD":
                case "QUADWRD":
                    Debug_WORDS(fieldName, this, method);
                    break;

                case "BINYDD":
                    Debug_BINYDD(fieldName, this, method);
                    break;

                case "VTOCXTENT":
                    Debug_VTOCXTENT(fieldName, this, method);
                    break;

                case "DSXTENT":
                    Debug_DSXTENT(fieldName, this, method);
                    break;

                case "FORMAT1_DSCB":
                    Debug_FORMAT1_DSCB(fieldName, this, method);
                    break;

                case "FORMAT4_DSCB":
                    Debug_FORMAT1_DSCB(fieldName, this, method);
                    break;

                case "VOL1_LABEL":
                    Debug_VOL1_LABEL(fieldName, this, method);
                    break;

                case "CKD_ImageFileDescriptor":
                    Debug_CKD_ImageFileDescriptor(fieldName, this, method);
                    break;

                case "LVL1TAB":
                    Debug_LVL1TAB(fieldName, this, method);
                    break;

                case "LVL2TAB":
                    Debug_LVL2TAB(fieldName, this, method);
                    break;

                case "LVL2ENTRY":
                    Debug_LVL2ENTRY(fieldName, this, method);
                    break;

                case "CCHHR":
                    Debug_CCHHR(fieldName, this, method);
                    break;

                case "CKDDASD_RECHDR":
                    Debug_CKDDASD_RECHDR(fieldName, this, method);
                    break;

                case "CKD_GROUP":
                    Debug_CKD_GROUP(fieldName, this, method);
                    break;

                case "DASD_DEVHDR":
                    Debug_DASD_DEVHDR(fieldName, this, method);
                    break;

                case "DASD_COMP_DEVHDR":
                    Debug_DASD_COMP_DEVHDR(fieldName, this, method);
                    break;

                case "FBADEV":
                    Debug_FBADEV(fieldName, this, method);
                    break;

                case "CKDDEV":
                    Debug_CKDDEV(fieldName, this, method);
                    break;

                case "DEVHND":
                    Debug_DEVHND(fieldName, this, method);
                    break;

                case "DASDEntry":
                    Debug_DASDEntry(fieldName, this, method);
                    break;

                case "MemberEntry":
                    Debug_MemberEntry(fieldName, this, method);
                    break;

                case "VolumeEntry":
                    Debug_VolumeEntry(fieldName, this, method);
                    break;

                case "DSNEntry":
                    Debug_DSNEntry(fieldName, this, method);
                    break;

                case "TRACKEntry":
                    Debug_TRACKEntry(fieldName, this, method);
                    break;

                case "CKD_HDR":
                    Debug_CKD_HDR(fieldName, this, method);
                    break;

                default:
                    Debug_UnknownEntry(fieldName, this, method);
                    break;
            }
        }

        private void Debug_UnknownEntry(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Type objType = @object.GetType();
            IEnumerable<FieldInfo> fieldInfo = objType.GetRuntimeFields();

            foreach (FieldInfo field in fieldInfo)
            {
                object value = field.GetValue(this);
                if (value != null)
                {
                    string vName = value.GetType().Name.ToLower();

                    switch (vName)
                    {
                        case "string":
                        case "int16":
                        case "int32":
                        case "int64":
                        case "int":
                        case "uint16":
                        case "uint32":
                        case "uint64":
                        case "uint":
                        case "byte":
                            Global.diag.Text += field.Name + ": " + value.ToString() + Environment.NewLine;
                            break;

                        default:
                            Global.diag.Text += field.Name + ": " + value.ToString() + Environment.NewLine;
                            break;
                    }
                }
                else
                {
                    Global.diag.Text += field.Name + ": [null]" + Environment.NewLine;
                }
            }
            Global.diag.Text += Environment.NewLine;
        }

        private void Debug_VolumeEntry(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;
        }

        private void Debug_BINYDD(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_DSXTENT(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_FORMAT1_DSCB(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_VOL1_LABEL(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_LVL1TAB(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_LVL2TAB(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_LVL2ENTRY(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_CCHHR(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_CKDDASD_RECHDR(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_CKD_GROUP(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_CKD_HDR(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_FBADEV(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_DEVHND(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_TRACKEntry(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_MemberEntry(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_DSNEntry(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }
        private void Debug_CKD_ImageFileDescriptor(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            Global.diag.Text += " // TODO: coding in progress" + Environment.NewLine; Global.diag.Text += Environment.NewLine;
        }

        private void Debug_DASD_DEVHDR(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;
        }

        private void Debug_DASD_COMP_DEVHDR(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;
        }

        private void Debug_CKDDEV(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;
        }

        private void Debug_WORDS(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;

            String clsName = @object.GetType().Name;

            switch (clsName)
            {
                case "HWORD":
                    HWORD hWORD = (HWORD)@object;
                    Global.diag.Text += "bytes: " + HexString(hWORD.bytes) + Environment.NewLine;
                    Global.diag.Text += "big_endian: " + hWORD.big_endian + Environment.NewLine;
                    Global.diag.Text += "little_endian: " + hWORD.little_endian + Environment.NewLine;
                    Global.diag.Text += Environment.NewLine;
                    break;

                case "FWORD":
                    FWORD fWORD = (FWORD)@object;
                    Global.diag.Text += "bytes: " + HexString(fWORD.bytes) + Environment.NewLine;
                    Global.diag.Text += "big_endian: " + fWORD.big_endian + Environment.NewLine;
                    Global.diag.Text += "little_endian: " + fWORD.little_endian + Environment.NewLine;
                    Global.diag.Text += Environment.NewLine;
                    break;

                case "DBLWRD":
                    DBLWRD dWORD = (DBLWRD)@object;
                    Global.diag.Text += "bytes: " + HexString(dWORD.bytes) + Environment.NewLine;
                    Global.diag.Text += "big_endian: " + dWORD.big_endian + Environment.NewLine;
                    Global.diag.Text += "little_endian: " + dWORD.little_endian + Environment.NewLine;
                    Global.diag.Text += Environment.NewLine;
                    break;

                case "QUADWRD":
                    QUADWRD qWORD = (QUADWRD)@object;
                    Global.diag.Text += "bytes: " + HexString(qWORD.bytes) + Environment.NewLine;
                    Global.diag.Text += "big_endian: " + qWORD.big_endian + Environment.NewLine;
                    Global.diag.Text += "little_endian: " + qWORD.little_endian + Environment.NewLine;
                    Global.diag.Text += Environment.NewLine;
                    break;

                default:
                    break;
            }
        }

        private void Debug_VTOCXTENT(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;
        }

        private void Debug_DEVBLK(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;
        }

        private void Debug_DASDEntry(String fieldName, extObject @object, MethodBase method)
        {
            Global.diag.Text += method.Name + ": " + @object.GetType().Name + ": " + fieldName + Environment.NewLine;
        }

        private String HexString(Byte[] bytes)
        {
            String ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += bytes[i].ToString("X2");
            }
            return ret;
        }
    }

    public class HWORD : extObject
    {
        public HWORD()
        {
            bytes = new Byte[SIZE];
        }
        public HWORD(params Byte[] b)
        {
            bytes = new Byte[SIZE];
            bytes[0] = b[0];
            bytes[1] = b[1];
        }
        public Byte[] bytes { get; set; }
        public Int16 little_endian
        {
            get
            {
                return (Int16)((bytes[1] << 8) | (bytes[0]));
            }
        }
        public Int16 big_endian
        {
            get
            {
                return (Int16)((bytes[0] << 8) | (bytes[1]));
            }
        }
        public int SIZE { get; } = 2;
        public void setHWORD(params Byte[] b)
        {
            if (bytes == null) { bytes = new Byte[SIZE]; }
            bytes[0] = b[0];
            bytes[1] = b[1];
        }
        public String ToHexString()
        {
            return bytes[0].ToString("X2") + bytes[1].ToString("X2");
        }

    }
    public class FWORD : extObject
    {
        public FWORD()
        {
            bytes = new Byte[SIZE];
        }
        public FWORD(params Byte[] b)
        {
            bytes = new Byte[SIZE];
            bytes[0] = b[0];
            bytes[1] = b[1];
            bytes[2] = b[2];
            bytes[3] = b[3];
        }
        public FWORD(Byte[] b, Int32 start)
        {
            bytes = new Byte[SIZE];
            bytes[0] = b[start];
            bytes[1] = b[start + 1];
            bytes[2] = b[start + 2];
            bytes[3] = b[start + 3];
        }
        public Byte[] bytes { get; set; }
        public Int32 little_endian
        {
            get
            {
                return ((bytes[3] << 24)
                    | (bytes[2] << 16)
                    | (bytes[1] << 8)
                    | (bytes[0]));
            }
        }
        public Int32 big_endian
        {
            get
            {
                return ((bytes[0] << 24)
                    | (bytes[1] << 16)
                    | (bytes[2] << 8)
                    | (bytes[3]));
            }
        }
        public int SIZE { get; } = 4;
        public void setFWORD(params Byte[] b)
        {
            if (bytes == null) { bytes = new Byte[SIZE]; }
            bytes[0] = b[0];
            bytes[1] = b[1];
            bytes[2] = b[2];
            bytes[3] = b[3];
        }
        public void setFWORD(Byte[] b, Int32 start)
        {
            if (bytes == null) { bytes = new Byte[SIZE]; }
            bytes[0] = b[start];
            bytes[1] = b[start + 1];
            bytes[2] = b[start + 2];
            bytes[3] = b[start + 3];
        }

        public String ToHexString()
        {
            String ret = "";
            for (int i = 0; i < SIZE; i++)
            {
                ret += bytes[i].ToString("X2");
            }
            return ret;
        }
    }
    public class DBLWRD : extObject
    {
        public DBLWRD()
        {
            bytes = new Byte[SIZE];
        }
        public DBLWRD(params Byte[] b)
        {
            bytes = new Byte[SIZE];
            Array.Copy(b, 0, bytes, 0, SIZE);
        }
        public DBLWRD(Byte[] b, Int32 start)
        {
            bytes = new Byte[SIZE];
            Array.Copy(b, start, bytes, 0, SIZE);
        }
        public Byte[] bytes { get; set; }
        public Int64 little_endian
        {
            get
            {
                return (Int64)((bytes[7] << 56)
                    | (bytes[6] << 48)
                    | (bytes[5] << 40)
                    | (bytes[4] << 32)
                    | (bytes[3] << 24)
                    | (bytes[2] << 16)
                    | (bytes[1] << 8)
                    | (bytes[0]));
            }
        }
        public Int64 big_endian
        {
            get
            {
                return (Int64)((bytes[0] << 56)
                    | (bytes[1] << 48)
                    | (bytes[2] << 40)
                    | (bytes[3] << 32)
                    | (bytes[4] << 24)
                    | (bytes[5] << 16)
                    | (bytes[6] << 8)
                    | (bytes[7]));
            }
        }
        public int SIZE { get; } = 8;

        public String ToHexString()
        {
            String ret = "";
            for (int i = 0; i < SIZE; i++)
            {
                ret += bytes[i].ToString("X2");
            }
            return ret;
        }
    }
    public class QUADWRD : extObject
    {
        public QUADWRD()
        {
            bytes = new Byte[SIZE];
        }
        public QUADWRD(params Byte[] b)
        {
            bytes = new Byte[SIZE];
            Array.Copy(b, 0, bytes, 0, SIZE);
        }
        public QUADWRD(Byte[] b, Int32 start)
        {
            bytes = new Byte[SIZE];
            Array.Copy(b, start, bytes, 0, SIZE);
        }
        public Byte[] bytes { get; set; }
        public Double little_endian
        {
            get
            {
                return (Double)((bytes[15] << 120)
                    | (bytes[14] << 112)
                    | (bytes[13] << 104)
                    | (bytes[12] << 96)
                    | (bytes[11] << 88)
                    | (bytes[10] << 80)
                    | (bytes[9] << 72)
                    | (bytes[8] << 64)
                    | (bytes[7] << 56)
                    | (bytes[6] << 48)
                    | (bytes[5] << 40)
                    | (bytes[4] << 32)
                    | (bytes[3] << 24)
                    | (bytes[2] << 16)
                    | (bytes[1] << 8)
                    | (bytes[0]));
            }
        }
        public Double big_endian
        {
            get
            {
                return (Double)((bytes[0] << 120)
                    | (bytes[1] << 112)
                    | (bytes[2] << 104)
                    | (bytes[3] << 96)
                    | (bytes[4] << 88)
                    | (bytes[5] << 80)
                    | (bytes[6] << 72)
                    | (bytes[7] << 64)
                    | (bytes[8] << 56)
                    | (bytes[9] << 48)
                    | (bytes[10] << 40)
                    | (bytes[11] << 32)
                    | (bytes[12] << 24)
                    | (bytes[13] << 16)
                    | (bytes[14] << 8)
                    | (bytes[15]));
            }
        }
        public int SIZE { get; } = 16;
        public String ToHexString()
        {
            String ret = "";
            for (int i = 0; i < SIZE; i++)
            {
                ret += bytes[i].ToString("X2");
            }
            return ret;
        }
    }

    public class BINYDD : extObject
    {
        public BINYDD()
        {
            bytes = new Byte[3];
        }
        public BINYDD(Byte[] b, Int32 start)
        {
            bytes = new Byte[3];
            Array.Copy(b, start, bytes, 0, 3);
        }
        public Byte[] bytes { get; set; }
        public Byte Y
        {
            get
            {
                return bytes[0];
            }
            set
            {
                bytes[0] = value;
            }
        }
        public HWORD DD
        {
            get
            {
                return new HWORD(bytes[1], bytes[2]);
            }
            set
            {
                bytes[1] = value.bytes[0];
                bytes[2] = value.bytes[1];
            }
        }
    }

    public class ERFHeader : extObject
    {
        public ERFHeader()
        {
            Bytes = new byte[160];
        }

        public override String ToString()
        {
            return "ERFHeader: " + FileType + " " + Version;
        }

        public Byte[] Bytes { get; set; }

        public String FileType
        {
            get
            {
                return Bytes.BytesToString(0, 4);
            }
            set
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i >= value.Length) return;
                    Bytes[i] = (byte)value[i];
                }
            }
        }

        public String Version
        {
            get
            {
                return Bytes.BytesToString(4, 4);
            }
            set
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i >= value.Length) return;
                    Bytes[4 + i] = (byte)value[i];
                }
            }
        }

        public Int32 LanguageCount
        {
            get
            {
                return Bytes.BytesToInt32(8);
            }
            set
            {
                Byte[] temp = BitConverter.GetBytes(value);
                for (int i = 0; i < temp.Length; i++)
                {
                    Bytes[8 + i] = temp[i];
                }
            }
        }

        public Int32 LocalizedStringSize
        {
            get
            {
                return Bytes.BytesToInt32(12);
            }
            set
            {
                Byte[] temp = BitConverter.GetBytes(value);
                for (int i = 0; i < temp.Length; i++)
                {
                    Bytes[12 + i] = temp[i];
                }
            }
        }

        public Int32 EntryCount
        {
            get
            {
                return Bytes.BytesToInt32(16);
            }
            set
            {
                Byte[] temp = BitConverter.GetBytes(value);
                for (int i = 0; i < temp.Length; i++)
                {
                    Bytes[16 + i] = temp[i];
                }
            }
        }

        public Int32 OffsetToLocalizedString
        {
            get
            {
                return Bytes.BytesToInt32(20);
            }
            set
            {
                Byte[] temp = BitConverter.GetBytes(value);
                for (int i = 0; i < temp.Length; i++)
                {
                    Bytes[20 + i] = temp[i];
                }
            }
        }

        public Int32 OffsetToKeyList
        {
            get
            {
                return Bytes.BytesToInt32(24);
            }
            set
            {
                Byte[] temp = BitConverter.GetBytes(value);
                for (int i = 0; i < temp.Length; i++)
                {
                    Bytes[24 + i] = temp[i];
                }
            }
        }

        public Int32 OffsetToResourceList
        {
            get
            {
                return Bytes.BytesToInt32(28);
            }
            set
            {
                Byte[] temp = BitConverter.GetBytes(value);
                for (int i = 0; i < temp.Length; i++)
                {
                    Bytes[28 + i] = temp[i];
                }
            }
        }

        public Int32 BuildYear
        {
            get
            {
                return Bytes.BytesToInt32(32);
            }
            set
            {
                Byte[] temp = BitConverter.GetBytes(value);
                for (int i = 0; i < temp.Length; i++)
                {
                    Bytes[32 + i] = temp[i];
                }
            }
        }

        public Int32 BuildDay
        {
            get
            {
                return Bytes.BytesToInt32(36);
            }
            set
            {
                Byte[] temp = BitConverter.GetBytes(value);
                for (int i = 0; i < temp.Length; i++)
                {
                    Bytes[36 + i] = temp[i];
                }
            }
        }

        public String DescriptionStrRef
        {
            get
            {
                return Bytes.BytesToString(40, 4);
            }
            set
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i >= value.Length) return;
                    Bytes[40 + i] = (byte)value[i];
                }
            }
        }
    }

    public class ERFLocalizedString : extObject
    {
        public ERFLocalizedString()
        {
            Bytes = new byte[9];
        }
        public ERFLocalizedString(int size)
        {
            Bytes = new byte[size];
        }

        public override String ToString()
        {
            return "ERFLocalizedString: LanguageID=" + LanguageID;
        }

        public Byte[] Bytes { get; set; }

        public Int32 LanguageID
        {
            get
            {
                return Bytes.BytesToInt32(0);
            }
            set
            {
                Byte[] temp = BitConverter.GetBytes(value);
                for (int i = 0; i < temp.Length; i++)
                {
                    Bytes[i] = temp[i];
                }
            }
        }

        public Int32 StringSize
        {
            get
            {
                return Bytes.BytesToInt32(4);
            }
            set
            {
                Byte[] temp = BitConverter.GetBytes(value);
                for (int i = 0; i < temp.Length; i++)
                {
                    Bytes[4 + i] = temp[i];
                }
            }
        }

        public String VariableString
        {
            get
            {
                return Bytes.BytesToString(8, StringSize);
            }
            set
            {
                for (int i = 0; i < StringSize; i++)
                {
                    if (i >= value.Length)
                    {
                        Bytes[8 + i] = 0;
                    }
                    else
                    {
                        Bytes[8 + i] = (byte)value[i];
                    }
                }
            }
        }
    }

    public class ERFKeyList : extObject
    {
        public ERFKeyList()
        {
            Bytes = new byte[24];
        }

        public override String ToString()
        {
            string str = "ERFKeyList: ResID=" + ResID;
            str += " ResType=" + ResType;
            str += " ResRef=" + ResRef.Trim().Trim('\0');
            str += " ResourceSize=" + resourceList.ResourceSize;
            return str;
        }

        public Byte[] Bytes { get; set; }

        public String ResRef
        {
            get
            {
                return Bytes.BytesToString(0, 16);
            }
            set
            {
                for (int i = 0; i < 16; i++)
                {
                    if (i >= value.Length)
                    {
                        Bytes[i] = 0;
                    }
                    else
                    {
                        Bytes[i] = (byte)value[i];
                    }
                }
            }
        }

        public Int32 ResID
        {
            get
            {
                return BitConverter.ToInt32(Bytes, 16);
            }
            set
            {
                Byte[] temp = BitConverter.GetBytes(value);
                for (int i = 0; i < temp.Length; i++)
                {
                    Bytes[16 + i] = temp[i];
                }
            }
        }

        public Int16 ResType
        {
            get
            {
                return BitConverter.ToInt16(Bytes, 20);
            }
            set
            {
                Byte[] temp = BitConverter.GetBytes(value);
                for (int i = 0; i < temp.Length; i++)
                {
                    Bytes[16 + i] = temp[i];
                }
            }
        }

        public ERFResourceList resourceList { get; set; }
    }

    public class ERFResourceList : extObject
    {
        public ERFResourceList()
        {
            Bytes = new byte[8];
        }

        public override String ToString()
        {
            return "ERFResourceList: " + OffsetToResource + " " + ResourceSize;
        }

        public Byte[] Bytes { get; set; }

        public Int32 OffsetToResource
        {
            get
            {
                return BitConverter.ToInt32(Bytes, 0);
            }
            set
            {
                Byte[] temp = BitConverter.GetBytes(value);
                for (int i = 0; i < temp.Length; i++)
                {
                    Bytes[i] = temp[i];
                }
            }
        }

        public Int32 ResourceSize
        {
            get
            {
                return BitConverter.ToInt32(Bytes, 4);
            }
            set
            {
                Byte[] temp = BitConverter.GetBytes(value);
                for (int i = 0; i < temp.Length; i++)
                {
                    Bytes[4 + i] = temp[i];
                }
            }
        }

        public ERFResourceData resourceData { get; set; }
    }

    public class ERFResourceData : extObject
    {
        public ERFResourceData()
        {
            Bytes = new byte[0];
        }

        public ERFResourceData(int size)
        {
            Bytes = new byte[size];
        }

        public override String ToString()
        {
            return "ERFResourceData: " + Bytes.Length;
        }

        public Byte[] Bytes { get; set; }
    }
}
