using System;

namespace WindowsCaptureVideo.Common
{
	// Token: 0x0200000C RID: 12
	public static class SYSTEM_WM
	{
		// Token: 0x04000031 RID: 49
		public const uint WM_ACTIVATE = 6U;

		// Token: 0x04000032 RID: 50
		public const uint WM_ACTIVATEAPP = 28U;

		// Token: 0x04000033 RID: 51
		public const uint WM_AFXFIRST = 864U;

		// Token: 0x04000034 RID: 52
		public const uint WM_AFXLAST = 895U;

		// Token: 0x04000035 RID: 53
		public const uint WM_APP = 32768U;

		// Token: 0x04000036 RID: 54
		public const uint WM_ASKCBFORMATNAME = 780U;

		// Token: 0x04000037 RID: 55
		public const uint WM_CANCELJOURNAL = 75U;

		// Token: 0x04000038 RID: 56
		public const uint WM_CANCELMODE = 31U;

		// Token: 0x04000039 RID: 57
		public const uint WM_CAPTURECHANGED = 533U;

		// Token: 0x0400003A RID: 58
		public const uint WM_CHANGECBCHAIN = 781U;

		// Token: 0x0400003B RID: 59
		public const uint WM_CHANGEUISTATE = 295U;

		// Token: 0x0400003C RID: 60
		public const uint WM_CHAR = 258U;

		// Token: 0x0400003D RID: 61
		public const uint WM_CHARTOITEM = 47U;

		// Token: 0x0400003E RID: 62
		public const uint WM_CHILDACTIVATE = 34U;

		// Token: 0x0400003F RID: 63
		public const uint WM_CLEAR = 771U;

		// Token: 0x04000040 RID: 64
		public const uint WM_CLOSE = 16U;

		// Token: 0x04000041 RID: 65
		public const uint WM_COMMAND = 273U;

		// Token: 0x04000042 RID: 66
		public const uint WM_COMPACTING = 65U;

		// Token: 0x04000043 RID: 67
		public const uint WM_COMPAREITEM = 57U;

		// Token: 0x04000044 RID: 68
		public const uint WM_CONTEXTMENU = 123U;

		// Token: 0x04000045 RID: 69
		public const uint WM_COPY = 769U;

		// Token: 0x04000046 RID: 70
		public const uint WM_COPYDATA = 74U;

		// Token: 0x04000047 RID: 71
		public const uint WM_CREATE = 1U;

		// Token: 0x04000048 RID: 72
		public const uint WM_CTLCOLORBTN = 309U;

		// Token: 0x04000049 RID: 73
		public const uint WM_CTLCOLORDLG = 310U;

		// Token: 0x0400004A RID: 74
		public const uint WM_CTLCOLOREDIT = 307U;

		// Token: 0x0400004B RID: 75
		public const uint WM_CTLCOLORLISTBOX = 308U;

		// Token: 0x0400004C RID: 76
		public const uint WM_CTLCOLORMSGBOX = 306U;

		// Token: 0x0400004D RID: 77
		public const uint WM_CTLCOLORSCROLLBAR = 311U;

		// Token: 0x0400004E RID: 78
		public const uint WM_CTLCOLORSTATIC = 312U;

		// Token: 0x0400004F RID: 79
		public const uint WM_CUT = 768U;

		// Token: 0x04000050 RID: 80
		public const uint WM_DEADCHAR = 259U;

		// Token: 0x04000051 RID: 81
		public const uint WM_DELETEITEM = 45U;

		// Token: 0x04000052 RID: 82
		public const uint WM_DESTROY = 2U;

		// Token: 0x04000053 RID: 83
		public const uint WM_DESTROYCLIPBOARD = 775U;

		// Token: 0x04000054 RID: 84
		public const uint WM_DEVICECHANGE = 537U;

		// Token: 0x04000055 RID: 85
		public const uint WM_DEVMODECHANGE = 27U;

		// Token: 0x04000056 RID: 86
		public const uint WM_DISPLAYCHANGE = 126U;

		// Token: 0x04000057 RID: 87
		public const uint WM_DRAWCLIPBOARD = 776U;

		// Token: 0x04000058 RID: 88
		public const uint WM_DRAWITEM = 43U;

		// Token: 0x04000059 RID: 89
		public const uint WM_DROPFILES = 563U;

		// Token: 0x0400005A RID: 90
		public const uint WM_ENABLE = 10U;

		// Token: 0x0400005B RID: 91
		public const uint WM_ENDSESSION = 22U;

		// Token: 0x0400005C RID: 92
		public const uint WM_ENTERIDLE = 289U;

		// Token: 0x0400005D RID: 93
		public const uint WM_ENTERMENULOOP = 529U;

		// Token: 0x0400005E RID: 94
		public const uint WM_ENTERSIZEMOVE = 561U;

		// Token: 0x0400005F RID: 95
		public const uint WM_ERASEBKGND = 20U;

		// Token: 0x04000060 RID: 96
		public const uint WM_EXITMENULOOP = 530U;

		// Token: 0x04000061 RID: 97
		public const uint WM_EXITSIZEMOVE = 562U;

		// Token: 0x04000062 RID: 98
		public const uint WM_FONTCHANGE = 29U;

		// Token: 0x04000063 RID: 99
		public const uint WM_GETDLGCODE = 135U;

		// Token: 0x04000064 RID: 100
		public const uint WM_GETFONT = 49U;

		// Token: 0x04000065 RID: 101
		public const uint WM_GETHOTKEY = 51U;

		// Token: 0x04000066 RID: 102
		public const uint WM_GETICON = 127U;

		// Token: 0x04000067 RID: 103
		public const uint WM_GETMINMAXINFO = 36U;

		// Token: 0x04000068 RID: 104
		public const uint WM_GETOBJECT = 61U;

		// Token: 0x04000069 RID: 105
		public const uint WM_GETTEXT = 13U;

		// Token: 0x0400006A RID: 106
		public const uint WM_GETTEXTLENGTH = 14U;

		// Token: 0x0400006B RID: 107
		public const uint WM_HANDHELDFIRST = 856U;

		// Token: 0x0400006C RID: 108
		public const uint WM_HANDHELDLAST = 863U;

		// Token: 0x0400006D RID: 109
		public const uint WM_HELP = 83U;

		// Token: 0x0400006E RID: 110
		public const uint WM_HOTKEY = 786U;

		// Token: 0x0400006F RID: 111
		public const uint WM_HSCROLL = 276U;

		// Token: 0x04000070 RID: 112
		public const uint WM_HSCROLLCLIPBOARD = 782U;

		// Token: 0x04000071 RID: 113
		public const uint WM_ICONERASEBKGND = 39U;

		// Token: 0x04000072 RID: 114
		public const uint WM_IME_CHAR = 646U;

		// Token: 0x04000073 RID: 115
		public const uint WM_IME_COMPOSITION = 271U;

		// Token: 0x04000074 RID: 116
		public const uint WM_IME_COMPOSITIONFULL = 644U;

		// Token: 0x04000075 RID: 117
		public const uint WM_IME_CONTROL = 643U;

		// Token: 0x04000076 RID: 118
		public const uint WM_IME_ENDCOMPOSITION = 270U;

		// Token: 0x04000077 RID: 119
		public const uint WM_IME_KEYDOWN = 656U;

		// Token: 0x04000078 RID: 120
		public const uint WM_IME_KEYLAST = 271U;

		// Token: 0x04000079 RID: 121
		public const uint WM_IME_KEYUP = 657U;

		// Token: 0x0400007A RID: 122
		public const uint WM_IME_NOTIFY = 642U;

		// Token: 0x0400007B RID: 123
		public const uint WM_IME_REQUEST = 648U;

		// Token: 0x0400007C RID: 124
		public const uint WM_IME_SELECT = 645U;

		// Token: 0x0400007D RID: 125
		public const uint WM_IME_SETCONTEXT = 641U;

		// Token: 0x0400007E RID: 126
		public const uint WM_IME_STARTCOMPOSITION = 269U;

		// Token: 0x0400007F RID: 127
		public const uint WM_INITDIALOG = 272U;

		// Token: 0x04000080 RID: 128
		public const uint WM_INITMENU = 278U;

		// Token: 0x04000081 RID: 129
		public const uint WM_INITMENUPOPUP = 279U;

		// Token: 0x04000082 RID: 130
		public const uint WM_INPUTLANGCHANGE = 81U;

		// Token: 0x04000083 RID: 131
		public const uint WM_INPUTLANGCHANGEREQUEST = 80U;

		// Token: 0x04000084 RID: 132
		public const uint WM_KEYDOWN = 256U;

		// Token: 0x04000085 RID: 133
		public const uint WM_KEYFIRST = 256U;

		// Token: 0x04000086 RID: 134
		public const uint WM_KEYLAST = 264U;

		// Token: 0x04000087 RID: 135
		public const uint WM_KEYUP = 257U;

		// Token: 0x04000088 RID: 136
		public const uint WM_KILLFOCUS = 8U;

		// Token: 0x04000089 RID: 137
		public const uint WM_LBUTTONDBLCLK = 515U;

		// Token: 0x0400008A RID: 138
		public const uint WM_LBUTTONDOWN = 513U;

		// Token: 0x0400008B RID: 139
		public const uint WM_LBUTTONUP = 514U;

		// Token: 0x0400008C RID: 140
		public const uint WM_MBUTTONDBLCLK = 521U;

		// Token: 0x0400008D RID: 141
		public const uint WM_MBUTTONDOWN = 519U;

		// Token: 0x0400008E RID: 142
		public const uint WM_MBUTTONUP = 520U;

		// Token: 0x0400008F RID: 143
		public const uint WM_MDIACTIVATE = 546U;

		// Token: 0x04000090 RID: 144
		public const uint WM_MDICASCADE = 551U;

		// Token: 0x04000091 RID: 145
		public const uint WM_MDICREATE = 544U;

		// Token: 0x04000092 RID: 146
		public const uint WM_MDIDESTROY = 545U;

		// Token: 0x04000093 RID: 147
		public const uint WM_MDIGETACTIVE = 553U;

		// Token: 0x04000094 RID: 148
		public const uint WM_MDIICONARRANGE = 552U;

		// Token: 0x04000095 RID: 149
		public const uint WM_MDIMAXIMIZE = 549U;

		// Token: 0x04000096 RID: 150
		public const uint WM_MDINEXT = 548U;

		// Token: 0x04000097 RID: 151
		public const uint WM_MDIREFRESHMENU = 564U;

		// Token: 0x04000098 RID: 152
		public const uint WM_MDIRESTORE = 547U;

		// Token: 0x04000099 RID: 153
		public const uint WM_MDISETMENU = 560U;

		// Token: 0x0400009A RID: 154
		public const uint WM_MDITILE = 550U;

		// Token: 0x0400009B RID: 155
		public const uint WM_MEASUREITEM = 44U;

		// Token: 0x0400009C RID: 156
		public const uint WM_MENUCHAR = 288U;

		// Token: 0x0400009D RID: 157
		public const uint WM_MENUCOMMAND = 294U;

		// Token: 0x0400009E RID: 158
		public const uint WM_MENUDRAG = 291U;

		// Token: 0x0400009F RID: 159
		public const uint WM_MENUGETOBJECT = 292U;

		// Token: 0x040000A0 RID: 160
		public const uint WM_MENURBUTTONUP = 290U;

		// Token: 0x040000A1 RID: 161
		public const uint WM_MENUSELECT = 287U;

		// Token: 0x040000A2 RID: 162
		public const uint WM_MOUSEACTIVATE = 33U;

		// Token: 0x040000A3 RID: 163
		public const uint WM_MOUSEFIRST = 512U;

		// Token: 0x040000A4 RID: 164
		public const uint WM_MOUSEHOVER = 673U;

		// Token: 0x040000A5 RID: 165
		public const uint WM_MOUSELAST = 525U;

		// Token: 0x040000A6 RID: 166
		public const uint WM_MOUSELEAVE = 675U;

		// Token: 0x040000A7 RID: 167
		public const uint WM_MOUSEMOVE = 512U;

		// Token: 0x040000A8 RID: 168
		public const uint WM_MOUSEWHEEL = 522U;

		// Token: 0x040000A9 RID: 169
		public const uint WM_MOUSEHWHEEL = 526U;

		// Token: 0x040000AA RID: 170
		public const uint WM_MOVE = 3U;

		// Token: 0x040000AB RID: 171
		public const uint WM_MOVING = 534U;

		// Token: 0x040000AC RID: 172
		public const uint WM_NCACTIVATE = 134U;

		// Token: 0x040000AD RID: 173
		public const uint WM_NCCALCSIZE = 131U;

		// Token: 0x040000AE RID: 174
		public const uint WM_NCCREATE = 129U;

		// Token: 0x040000AF RID: 175
		public const uint WM_NCDESTROY = 130U;

		// Token: 0x040000B0 RID: 176
		public const uint WM_NCHITTEST = 132U;

		// Token: 0x040000B1 RID: 177
		public const uint WM_NCLBUTTONDBLCLK = 163U;

		// Token: 0x040000B2 RID: 178
		public const uint WM_NCLBUTTONDOWN = 161U;

		// Token: 0x040000B3 RID: 179
		public const uint WM_NCLBUTTONUP = 162U;

		// Token: 0x040000B4 RID: 180
		public const uint WM_NCMBUTTONDBLCLK = 169U;

		// Token: 0x040000B5 RID: 181
		public const uint WM_NCMBUTTONDOWN = 167U;

		// Token: 0x040000B6 RID: 182
		public const uint WM_NCMBUTTONUP = 168U;

		// Token: 0x040000B7 RID: 183
		public const uint WM_NCMOUSEHOVER = 672U;

		// Token: 0x040000B8 RID: 184
		public const uint WM_NCMOUSELEAVE = 674U;

		// Token: 0x040000B9 RID: 185
		public const uint WM_NCMOUSEMOVE = 160U;

		// Token: 0x040000BA RID: 186
		public const uint WM_NCPAINT = 133U;

		// Token: 0x040000BB RID: 187
		public const uint WM_NCRBUTTONDBLCLK = 166U;

		// Token: 0x040000BC RID: 188
		public const uint WM_NCRBUTTONDOWN = 164U;

		// Token: 0x040000BD RID: 189
		public const uint WM_NCRBUTTONUP = 165U;

		// Token: 0x040000BE RID: 190
		public const uint WM_NCXBUTTONDBLCLK = 173U;

		// Token: 0x040000BF RID: 191
		public const uint WM_NCXBUTTONDOWN = 171U;

		// Token: 0x040000C0 RID: 192
		public const uint WM_NCXBUTTONUP = 172U;

		// Token: 0x040000C1 RID: 193
		public const uint WM_NCUAHDRAWCAPTION = 174U;

		// Token: 0x040000C2 RID: 194
		public const uint WM_NCUAHDRAWFRAME = 175U;

		// Token: 0x040000C3 RID: 195
		public const uint WM_NEXTDLGCTL = 40U;

		// Token: 0x040000C4 RID: 196
		public const uint WM_NEXTMENU = 531U;

		// Token: 0x040000C5 RID: 197
		public const uint WM_NOTIFY = 78U;

		// Token: 0x040000C6 RID: 198
		public const uint WM_NOTIFYFORMAT = 85U;

		// Token: 0x040000C7 RID: 199
		public const uint WM_NULL = 0U;

		// Token: 0x040000C8 RID: 200
		public const uint WM_PAINT = 15U;

		// Token: 0x040000C9 RID: 201
		public const uint WM_PAINTCLIPBOARD = 777U;

		// Token: 0x040000CA RID: 202
		public const uint WM_PAINTICON = 38U;

		// Token: 0x040000CB RID: 203
		public const uint WM_PALETTECHANGED = 785U;

		// Token: 0x040000CC RID: 204
		public const uint WM_PALETTEISCHANGING = 784U;

		// Token: 0x040000CD RID: 205
		public const uint WM_PARENTNOTIFY = 528U;

		// Token: 0x040000CE RID: 206
		public const uint WM_PASTE = 770U;

		// Token: 0x040000CF RID: 207
		public const uint WM_PENWINFIRST = 896U;

		// Token: 0x040000D0 RID: 208
		public const uint WM_PENWINLAST = 911U;

		// Token: 0x040000D1 RID: 209
		public const uint WM_POWER = 72U;

		// Token: 0x040000D2 RID: 210
		public const uint WM_POWERBROADCAST = 536U;

		// Token: 0x040000D3 RID: 211
		public const uint WM_PRINT = 791U;

		// Token: 0x040000D4 RID: 212
		public const uint WM_PRINTCLIENT = 792U;

		// Token: 0x040000D5 RID: 213
		public const uint WM_QUERYDRAGICON = 55U;

		// Token: 0x040000D6 RID: 214
		public const uint WM_QUERYENDSESSION = 17U;

		// Token: 0x040000D7 RID: 215
		public const uint WM_QUERYNEWPALETTE = 783U;

		// Token: 0x040000D8 RID: 216
		public const uint WM_QUERYOPEN = 19U;

		// Token: 0x040000D9 RID: 217
		public const uint WM_QUEUESYNC = 35U;

		// Token: 0x040000DA RID: 218
		public const uint WM_QUIT = 18U;

		// Token: 0x040000DB RID: 219
		public const uint WM_RBUTTONDBLCLK = 518U;

		// Token: 0x040000DC RID: 220
		public const uint WM_RBUTTONDOWN = 516U;

		// Token: 0x040000DD RID: 221
		public const uint WM_RBUTTONUP = 517U;

		// Token: 0x040000DE RID: 222
		public const uint WM_RENDERALLFORMATS = 774U;

		// Token: 0x040000DF RID: 223
		public const uint WM_RENDERFORMAT = 773U;

		// Token: 0x040000E0 RID: 224
		public const uint WM_SETCURSOR = 32U;

		// Token: 0x040000E1 RID: 225
		public const uint WM_SETFOCUS = 7U;

		// Token: 0x040000E2 RID: 226
		public const uint WM_SETFONT = 48U;

		// Token: 0x040000E3 RID: 227
		public const uint WM_SETHOTKEY = 50U;

		// Token: 0x040000E4 RID: 228
		public const uint WM_SETICON = 128U;

		// Token: 0x040000E5 RID: 229
		public const uint WM_SETREDRAW = 11U;

		// Token: 0x040000E6 RID: 230
		public const uint WM_SETTEXT = 12U;

		// Token: 0x040000E7 RID: 231
		public const uint WM_SETTINGCHANGE = 26U;

		// Token: 0x040000E8 RID: 232
		public const uint WM_SHOWWINDOW = 24U;

		// Token: 0x040000E9 RID: 233
		public const uint WM_SIZE = 5U;

		// Token: 0x040000EA RID: 234
		public const uint WM_SIZECLIPBOARD = 779U;

		// Token: 0x040000EB RID: 235
		public const uint WM_SIZING = 532U;

		// Token: 0x040000EC RID: 236
		public const uint WM_SPOOLERSTATUS = 42U;

		// Token: 0x040000ED RID: 237
		public const uint WM_STYLECHANGED = 125U;

		// Token: 0x040000EE RID: 238
		public const uint WM_STYLECHANGING = 124U;

		// Token: 0x040000EF RID: 239
		public const uint WM_SYNCPAINT = 136U;

		// Token: 0x040000F0 RID: 240
		public const uint WM_SYSCHAR = 262U;

		// Token: 0x040000F1 RID: 241
		public const uint WM_SYSCOLORCHANGE = 21U;

		// Token: 0x040000F2 RID: 242
		public const uint WM_SYSCOMMAND = 274U;

		// Token: 0x040000F3 RID: 243
		public const uint WM_SYSDEADCHAR = 263U;

		// Token: 0x040000F4 RID: 244
		public const uint WM_SYSKEYDOWN = 260U;

		// Token: 0x040000F5 RID: 245
		public const uint WM_SYSKEYUP = 261U;

		// Token: 0x040000F6 RID: 246
		public const uint WM_TCARD = 82U;

		// Token: 0x040000F7 RID: 247
		public const uint WM_TIMECHANGE = 30U;

		// Token: 0x040000F8 RID: 248
		public const uint WM_TIMER = 275U;

		// Token: 0x040000F9 RID: 249
		public const uint WM_UNDO = 772U;

		// Token: 0x040000FA RID: 250
		public const uint WM_UNINITMENUPOPUP = 293U;

		// Token: 0x040000FB RID: 251
		public const uint WM_USER = 1024U;

		// Token: 0x040000FC RID: 252
		public const uint WM_USERCHANGED = 84U;

		// Token: 0x040000FD RID: 253
		public const uint WM_VKEYTOITEM = 46U;

		// Token: 0x040000FE RID: 254
		public const uint WM_VSCROLL = 277U;

		// Token: 0x040000FF RID: 255
		public const uint WM_VSCROLLCLIPBOARD = 778U;

		// Token: 0x04000100 RID: 256
		public const uint WM_WINDOWPOSCHANGED = 71U;

		// Token: 0x04000101 RID: 257
		public const uint WM_WINDOWPOSCHANGING = 70U;

		// Token: 0x04000102 RID: 258
		public const uint WM_WININICHANGE = 26U;

		// Token: 0x04000103 RID: 259
		public const uint WM_XBUTTONDBLCLK = 525U;

		// Token: 0x04000104 RID: 260
		public const uint WM_XBUTTONDOWN = 523U;

		// Token: 0x04000105 RID: 261
		public const uint WM_XBUTTONUP = 524U;
	}
}
