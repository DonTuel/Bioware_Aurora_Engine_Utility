using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Bioware_Aurora_Engine_Utility
{
    public class ERF_Routines
    {
        public static int totalLists;
        public static int curCount;
        public static Form frmPgBar;
        public static StreamReader rdr;
        public static ERFHeader erfHeader;
        public static ListBox listBox;
        public static ProgressBar progressBar;

        public static bool cancelLoad;

        public static bool Open_File(string fileName)
        {
            try
            {
                rdr = new StreamReader(fileName);

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error opening file");
                return false;
            }
        }

        public static void Process_Open_File()
        {
            listBox.Items.Clear();

            rdr.BaseStream.Position = 0;
            erfHeader = new ERFHeader();

            rdr.BaseStream.Read(erfHeader.Bytes, 0, erfHeader.Bytes.Length);

            totalLists = erfHeader.LanguageCount + erfHeader.EntryCount;
            curCount = 0;

            progressBar.Minimum = 0;
            progressBar.Maximum = totalLists;

            listBox.Items.Add(erfHeader);

            if (erfHeader.LanguageCount > 0 && erfHeader.LocalizedStringSize > 0)
            {
                Int32 curOff = erfHeader.OffsetToLocalizedString;
                Int32 remSize = erfHeader.LocalizedStringSize;

                for (int i = 0; i < erfHeader.LanguageCount; i++)
                {
                    ERFLocalizedString sleStruct = new ERFLocalizedString();
                    rdr.BaseStream.Position = curOff;
                    rdr.BaseStream.Read(sleStruct.Bytes, 0, 8);
                    sleStruct = new ERFLocalizedString(sleStruct.StringSize + 8);
                    rdr.BaseStream.Position = curOff;
                    rdr.BaseStream.Read(sleStruct.Bytes, 0, sleStruct.Bytes.Length);
                    listBox.Items.Add(sleStruct);
                    curOff += 8 + sleStruct.StringSize;
                    remSize -= (8 + sleStruct.StringSize);

                    curCount++;
                    progressBar.Value = curCount;

                    Application.DoEvents();
                    if (cancelLoad) return;
                }
            }

            if (erfHeader.EntryCount > 0)
            {
                Int32 keyOff = erfHeader.OffsetToKeyList;
                Int32 resOff = erfHeader.OffsetToResourceList;

                for (int i = 0; i < erfHeader.EntryCount; i++)
                {
                    ERFKeyList keyStruct = new ERFKeyList();
                    rdr.BaseStream.Position = keyOff;
                    rdr.BaseStream.Read(keyStruct.Bytes, 0, keyStruct.Bytes.Length);
                    keyOff += keyStruct.Bytes.Length;

                    ERFResourceList rleStruct = new ERFResourceList();
                    rdr.BaseStream.Position = resOff;
                    rdr.BaseStream.Read(rleStruct.Bytes, 0, rleStruct.Bytes.Length);
                    resOff += 8;

                    ERFResourceData resData = new ERFResourceData(rleStruct.ResourceSize);
                    rdr.BaseStream.Position = rleStruct.OffsetToResource;
                    rdr.BaseStream.Read(resData.Bytes, 0, rleStruct.ResourceSize);
                    keyStruct.resourceList = rleStruct;
                    keyStruct.resourceList.resourceData = resData;
                    listBox.Items.Add(keyStruct);

                    curCount++;
                    progressBar.Value = curCount;

                    Application.DoEvents();
                    if (cancelLoad) return;
                }
            }
        }

        public static void Dump_ERFStructure(ERFHeader erfHeader, TabPage tabPage)
        {
            char[] anyDelim = { '/', '\\' };
            RichTextBox textBox = Locate_RichTextBox(tabPage);
            tabPage.Text = "ERF Header";

            String rtfStr = Global._rtfHdr;
            //rtfStr += "{\\b ERF Header}" + Global._rtfNL;
            rtfStr += ERF_Routines.Hex_Dump(erfHeader.Bytes);

            if (textBox != null) textBox.Rtf = rtfStr;
        }

        public static void Dump_ERFStructure(ERFLocalizedString erfLocalString, TabPage tabPage)
        {
            char[] anyDelim = { '/', '\\' };
            RichTextBox textBox = Locate_RichTextBox(tabPage);
            tabPage.Text = "String List";

            String rtfStr = Global._rtfHdr;

            rtfStr += "{\\b LanguageID=" + erfLocalString.LanguageID.ToString() + "}" + Global._rtfNL;
            rtfStr += ERF_Routines.Hex_Dump(erfLocalString.Bytes);

            if (textBox != null) textBox.Rtf = rtfStr;
        }

        public static void Dump_ERFStructure(ERFKeyList erfKeyList, TabPage[] tabPages)
        {
            char[] anyDelim = { '/', '\\' };

            int i = 0;
            String rtfStr = Global._rtfHdr;
            RichTextBox textBox = Locate_RichTextBox(tabPages[i]);

            if (tabPages.Length > 1)
            {
                tabPages[i].Text = "Key List ResID=" + erfKeyList.ResID.ToString();
            }
            else
            {
                tabPages[i].Text = "ERF Key and Resource";
                rtfStr += "{\\b Key List Element ResID=" + erfKeyList.ResID.ToString() + "}" + Global._rtfNL;
            }

            rtfStr += Hex_Dump(erfKeyList.Bytes);

            if (tabPages.Length > 1)
            {
                textBox.Rtf = rtfStr;
                i++;
                rtfStr = Global._rtfHdr;
                textBox = Locate_RichTextBox(tabPages[i]);
                tabPages[i].Text = "Resource List ResID=" + erfKeyList.ResID.ToString();
            }
            else
            {
                rtfStr += "{\\b Resource List Element ResID=" + erfKeyList.ResID.ToString() + "}" + Global._rtfNL;
            }

            rtfStr += Hex_Dump(erfKeyList.resourceList.Bytes);

            if (tabPages.Length > 2)
            {
                textBox.Rtf = rtfStr;
                i++;
                rtfStr = Global._rtfHdr;
                textBox = Locate_RichTextBox(tabPages[i]);
                tabPages[i].Text = "Resource Data ResID=" + erfKeyList.ResID.ToString();
            }
            else
            {
                rtfStr += "{\\b Resource Data ResID=" + erfKeyList.ResID.ToString() + "}" + Global._rtfNL;
            }

            rtfStr += Format_ResourceData(erfKeyList);

            textBox.Rtf = rtfStr;
            textBox.SelectionStart = 0;
            textBox.Focus();
            textBox.ScrollToCaret();
        }

        public static String Format_ResourceData(ERFKeyList erfKeyList)
        {
            switch (erfKeyList.ResType)
            {
                case 1: // Windows BMP
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 3: // TGA image
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 4: // WAV sound
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 6: // Bioware Packed Layered Texture
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 7: // Windows INI
                    return ERF_Routines.Text_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 10: // Text
                    return ERF_Routines.Text_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2002: // Aurora model
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2009: // NW script source
                    return ERF_Routines.Text_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2010: // NW compiled script
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2012: // Bioware Aurora Engine Area data
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2013: // Bioware Aurora Engine Tileset
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2014: // Module Info data
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2015: // Character/Creature
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2016: // Walkmesh
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2017: // 2D array
                    return ERF_Routines.Text_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2022: // Extra Texture Info
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2023: // Game Instance
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2025: // Item Blueprint
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2027: // Creature Blueprint
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2029: // Conversation
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2030: // Tile/Blueprint Palette
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2032: // Trigger Blueprint
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2033: // Compressed texture
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2035: // Sound Blueprint
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2036: // Letter-combo probability infor for name generation
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2037: // Generic File Format
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2038: // Faction
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2040: // Encounter Blueprint
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2042: // Door Blueprint
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2044: // Placeable Object Blueprint
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2045: // Default Values
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2046: // Game Instance Comments
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2047: // Graphical User Interface layout
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2051: // Store/Merchant Blueprint
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2052: // Door walkmesh
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2053: // Placeable Object walkmesh
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2056: // Journal
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2058: // Waypoint Blueprint
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2060: // Sound Set
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2064: // Script Debugger
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2065: // Plot Manager/Instance
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                case 2066: // Plot Wizard Blueprint
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
                default:
                    return ERF_Routines.Hex_Dump(erfKeyList.resourceList.resourceData.Bytes);
            }
        }

        public static RichTextBox Locate_RichTextBox(TabPage tabPage)
        {
            RichTextBox textBox = null;

            foreach (var item in tabPage.Controls)
            {
                if (item.GetType().ToString().Contains("RichTextBox"))
                {
                    textBox = (RichTextBox)item;
                }
            }

            return textBox;
        }

        public static String Text_Dump(Byte[] buf, Int32 buflen)
        {
            String retStr = "";
            Int32 i = 0;

            while (i < buflen)
            {
                switch (buf[i])
                {
                    case 0x0a:
                        retStr += Global._rtfNL;
                        break;
                    case 0x0d:
                        break;
                    default:
                        retStr += (Char)buf[i];
                        break;
                }
                i++;
            }

            return retStr;
        }

        public static String Text_Dump(Byte[] buf)
        {
            return Text_Dump(buf, buf.Length);
        }

        public static String Hex_Dump_Header()
        {
            String retStr = "";

            retStr += "{ <Offset> ";

            for (int i = 0; i < Global._columnWidth; i++)
            {
                retStr += " " + i.ToString("X2");
            }
            retStr += " |";
            for (int i = 0; i < Global._columnWidth; i++)
            {
                if ((i % 8) == 7)
                {
                    retStr += "+";
                }
                else
                {
                    retStr += ".";
                }
            }
            retStr += "|";
            retStr += Global._rtfNL;

            return retStr;
        }

        public static String Hex_Dump(Byte[] buf, Int32 buflen)
        {
            String retStr = "";
            String retSStr = "";
            Int32 widthM1 = Global._columnWidth - 1;

            String retCStr = "  ";
            int k = buflen;
            int j;

            retSStr += " {\\b 00000000} ";

            for (j = 0; j < k; j++)
            {
                retSStr += " " + buf[j].ToString("X2");
                Byte c = (Byte)ERF_Routines.ByteToChar(buf[j]);
                retCStr = retCStr + "\\'" + c.ToString("x2");
                if ((j & widthM1) == widthM1)
                {
                    retStr += retSStr + retCStr + Global._rtfNL;
                    if (j < (k - 1))
                    {
                        retSStr = " {\\b " + (j + 1).ToString("X8") + "} ";
                        retCStr = "  ";
                    }
                    else
                    {
                        retSStr = "";
                        retCStr = "";
                    }
                }
            }
            j--;
            j &= widthM1;
            if (j > 0)
            {
                for (int jj = j; jj < widthM1; jj++)
                {
                    retSStr += "   ";
                }
            }
            if (!((retSStr == "") && (retCStr == "")))
            {
                retStr += retSStr + retCStr + Global._rtfNL;
            }

            retStr += Global._rtfNL;

            return retStr;
        }

        public static String Hex_Dump(byte[] buf)
        {
            return Hex_Dump(buf, buf.Length);
        }

        public static Char ByteToChar(Byte byt)
        {
            if ((byt > 0x1f) && (byt < 0xff)) { return (Char)byt; }
            return '.';
        }
    }
}
