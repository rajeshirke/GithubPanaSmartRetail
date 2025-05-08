; ModuleID = 'obj/Debug/android/marshal_methods.armeabi-v7a.ll'
source_filename = "obj/Debug/android/marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android"


%struct.MonoImage = type opaque

%struct.MonoClass = type opaque

%struct.MarshalMethodsManagedClass = type {
	i32,; uint32_t token
	%struct.MonoClass*; MonoClass* klass
}

%struct.MarshalMethodName = type {
	i64,; uint64_t id
	i8*; char* name
}

%class._JNIEnv = type opaque

%class._jobject = type {
	i8; uint8_t b
}

%class._jclass = type {
	i8; uint8_t b
}

%class._jstring = type {
	i8; uint8_t b
}

%class._jthrowable = type {
	i8; uint8_t b
}

%class._jarray = type {
	i8; uint8_t b
}

%class._jobjectArray = type {
	i8; uint8_t b
}

%class._jbooleanArray = type {
	i8; uint8_t b
}

%class._jbyteArray = type {
	i8; uint8_t b
}

%class._jcharArray = type {
	i8; uint8_t b
}

%class._jshortArray = type {
	i8; uint8_t b
}

%class._jintArray = type {
	i8; uint8_t b
}

%class._jlongArray = type {
	i8; uint8_t b
}

%class._jfloatArray = type {
	i8; uint8_t b
}

%class._jdoubleArray = type {
	i8; uint8_t b
}

; assembly_image_cache
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 4
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [560 x i32] [
	i32 5009434, ; 0: Syncfusion.Cards.XForms.Android => 0x4c701a => 87
	i32 6293305, ; 1: Syncfusion.SfRadialMenu.Android => 0x600739 => 113
	i32 11257817, ; 2: OxyPlot.dll => 0xabc7d9 => 61
	i32 32687329, ; 3: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 198
	i32 34715100, ; 4: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 240
	i32 39109920, ; 5: Newtonsoft.Json.dll => 0x254c520 => 58
	i32 39852199, ; 6: Plugin.Permissions => 0x26018a7 => 69
	i32 57263871, ; 7: Xamarin.Forms.Core.dll => 0x369c6ff => 232
	i32 57967248, ; 8: Xamarin.Android.Support.VersionedParcelable.dll => 0x3748290 => 163
	i32 90921095, ; 9: Syncfusion.SfNumericTextBox.XForms.Android => 0x56b5887 => 108
	i32 99762151, ; 10: Syncfusion.Buttons.XForms.dll => 0x5f23fe7 => 86
	i32 101534019, ; 11: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 217
	i32 103826079, ; 12: XF.Material.dll => 0x630429f => 253
	i32 117431740, ; 13: System.Runtime.InteropServices => 0x6ffddbc => 276
	i32 120558881, ; 14: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 217
	i32 122350210, ; 15: System.Threading.Channels.dll => 0x74aea82 => 128
	i32 134690465, ; 16: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 248
	i32 148395041, ; 17: Lottie.Forms.dll => 0x8d85421 => 28
	i32 160529393, ; 18: Xamarin.Android.Arch.Core.Common => 0x9917bf1 => 133
	i32 165246403, ; 19: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 175
	i32 166922606, ; 20: Xamarin.Android.Support.Compat.dll => 0x9f3096e => 144
	i32 172012715, ; 21: FastAndroidCamera.dll => 0xa40b4ab => 18
	i32 182336117, ; 22: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 221
	i32 192036811, ; 23: Octane.Xamarin.Forms.VideoPlayer => 0xb723fcb => 60
	i32 201930040, ; 24: Xamarin.Android.Arch.Core.Runtime => 0xc093538 => 134
	i32 209399409, ; 25: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 173
	i32 212497893, ; 26: Xamarin.Forms.Maps.Android => 0xcaa75e5 => 234
	i32 219130465, ; 27: Xamarin.Android.Support.v4 => 0xd0faa61 => 160
	i32 220171995, ; 28: System.Diagnostics.Debug => 0xd1f8edb => 5
	i32 221063263, ; 29: Microsoft.AspNetCore.Http.Connections.Client => 0xd2d285f => 39
	i32 230216969, ; 30: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 192
	i32 230752869, ; 31: Microsoft.CSharp.dll => 0xdc10265 => 47
	i32 231814094, ; 32: System.Globalization => 0xdd133ce => 267
	i32 232815796, ; 33: System.Web.Services => 0xde07cb4 => 273
	i32 261689757, ; 34: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 178
	i32 278686392, ; 35: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 196
	i32 280482487, ; 36: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 190
	i32 287869112, ; 37: Syncfusion.SfChart.XForms.dll => 0x112888b8 => 102
	i32 307891448, ; 38: Xamarin.AndroidX.Work.Runtime.dll => 0x125a0cf8 => 229
	i32 318968648, ; 39: Xamarin.AndroidX.Activity.dll => 0x13031348 => 165
	i32 319314094, ; 40: Xamarin.Forms.Maps => 0x130858ae => 235
	i32 321597661, ; 41: System.Numerics => 0x132b30dd => 121
	i32 332531999, ; 42: Syncfusion.SfBusyIndicator.XForms.Android => 0x13d2091f => 99
	i32 334355562, ; 43: ZXing.Net.Mobile.Forms.dll => 0x13eddc6a => 256
	i32 342366114, ; 44: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 194
	i32 347068432, ; 45: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 82
	i32 348048101, ; 46: Microsoft.AspNetCore.Http.Connections.Common.dll => 0x14becae5 => 40
	i32 377964854, ; 47: Syncfusion.SfAutoComplete.XForms => 0x16874936 => 97
	i32 385762202, ; 48: System.Memory.dll => 0x16fe439a => 268
	i32 388313361, ; 49: Xamarin.Android.Support.Animated.Vector.Drawable => 0x17253111 => 140
	i32 389971796, ; 50: Xamarin.Android.Support.Core.UI => 0x173e7f54 => 146
	i32 402672763, ; 51: Xamarin.Plugin.Calendar => 0x18004c7b => 252
	i32 418843730, ; 52: Retail.Infrastructure => 0x18f70c52 => 1
	i32 441335492, ; 53: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 177
	i32 442521989, ; 54: Xamarin.Essentials => 0x1a605985 => 231
	i32 442565967, ; 55: System.Collections => 0x1a61054f => 3
	i32 450948140, ; 56: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 189
	i32 458494020, ; 57: Microsoft.AspNetCore.SignalR.Common.dll => 0x1b541044 => 44
	i32 465846621, ; 58: mscorlib => 0x1bc4415d => 56
	i32 469710990, ; 59: System.dll => 0x1bff388e => 119
	i32 476646585, ; 60: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 190
	i32 486930444, ; 61: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 203
	i32 489724623, ; 62: Xamarin.Forms.DebugRainbows.dll => 0x1d309acf => 233
	i32 498788369, ; 63: System.ObjectModel => 0x1dbae811 => 261
	i32 513247710, ; 64: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 53
	i32 514659665, ; 65: Xamarin.Android.Support.Compat => 0x1ead1551 => 144
	i32 520798577, ; 66: FFImageLoading.Platform => 0x1f0ac171 => 22
	i32 524864063, ; 67: Xamarin.Android.Support.Print => 0x1f48ca3f => 157
	i32 525008092, ; 68: SkiaSharp.dll => 0x1f4afcdc => 76
	i32 526420162, ; 69: System.Transactions.dll => 0x1f6088c2 => 259
	i32 527452488, ; 70: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 248
	i32 535134469, ; 71: Syncfusion.SfBusyIndicator.Android => 0x1fe58105 => 98
	i32 539058512, ; 72: Microsoft.Extensions.Logging => 0x20216150 => 51
	i32 544275819, ; 73: Retail.Infrastructure.dll => 0x2070fd6b => 1
	i32 545304856, ; 74: System.Runtime.Extensions => 0x2080b118 => 7
	i32 548916678, ; 75: Microsoft.Bcl.AsyncInterfaces => 0x20b7cdc6 => 46
	i32 555173402, ; 76: Syncfusion.SfPicker.XForms.Android => 0x2117461a => 111
	i32 605376203, ; 77: System.IO.Compression.FileSystem => 0x24154ecb => 271
	i32 610194910, ; 78: System.Reactive.dll => 0x245ed5de => 123
	i32 627609679, ; 79: Xamarin.AndroidX.CustomView => 0x2568904f => 183
	i32 639843206, ; 80: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 188
	i32 662205335, ; 81: System.Text.Encodings.Web.dll => 0x27787397 => 126
	i32 663517072, ; 82: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 226
	i32 666292255, ; 83: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 170
	i32 672442732, ; 84: System.Collections.Concurrent => 0x2814a96c => 12
	i32 690569205, ; 85: System.Xml.Linq.dll => 0x29293ff5 => 130
	i32 691348768, ; 86: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 250
	i32 692692150, ; 87: Xamarin.Android.Support.Annotations => 0x2949a4b6 => 141
	i32 700284507, ; 88: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 245
	i32 707987836, ; 89: Syncfusion.Cards.XForms.dll => 0x2a33097c => 88
	i32 708149616, ; 90: Syncfusion.Data.Portable.dll => 0x2a358170 => 91
	i32 719061231, ; 91: Syncfusion.Core.XForms.dll => 0x2adc00ef => 90
	i32 720511267, ; 92: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 249
	i32 721481609, ; 93: Microsoft.AppCenter.dll => 0x2b00ef89 => 37
	i32 729844822, ; 94: Syncfusion.SfComboBox.XForms.Android => 0x2b808c56 => 103
	i32 732016536, ; 95: Shiny.Core => 0x2ba1af98 => 72
	i32 748832960, ; 96: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 80
	i32 775507847, ; 97: System.IO.Compression => 0x2e394f87 => 270
	i32 789151979, ; 98: Microsoft.Extensions.Options => 0x2f0980eb => 52
	i32 791612551, ; 99: DLToolkit.Forms.Controls.FlowListView => 0x2f2f0c87 => 17
	i32 801787702, ; 100: Xamarin.Android.Support.Interpolator => 0x2fca4f36 => 153
	i32 802720955, ; 101: SignaturePad => 0x2fd88cbb => 74
	i32 806593955, ; 102: Plugin.DeviceInfo => 0x3013a5a3 => 66
	i32 809851609, ; 103: System.Drawing.Common.dll => 0x30455ad9 => 10
	i32 832711436, ; 104: Microsoft.AspNetCore.SignalR.Protocols.Json.dll => 0x31a22b0c => 45
	i32 836755697, ; 105: Xamarin.AndroidX.Lifecycle.Service => 0x31dfe0f1 => 199
	i32 843511501, ; 106: Xamarin.AndroidX.Print => 0x3246f6cd => 210
	i32 877678880, ; 107: System.Globalization.dll => 0x34505120 => 267
	i32 882883187, ; 108: Xamarin.Android.Support.v4.dll => 0x349fba73 => 160
	i32 886248193, ; 109: Microcharts.Droid => 0x34d31301 => 30
	i32 898563296, ; 110: Microsoft.AppCenter.Crashes.Android.Bindings => 0x358efce0 => 35
	i32 902159924, ; 111: Rg.Plugins.Popup => 0x35c5de34 => 71
	i32 903406257, ; 112: SignaturePad.dll => 0x35d8e2b1 => 74
	i32 916714535, ; 113: Xamarin.Android.Support.Print.dll => 0x36a3f427 => 157
	i32 928116545, ; 114: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 240
	i32 954320159, ; 115: ZXing.Net.Mobile.Forms => 0x38e1c51f => 256
	i32 954665006, ; 116: Syncfusion.SfRadialMenu.XForms.Android.dll => 0x38e7082e => 114
	i32 955402788, ; 117: Newtonsoft.Json => 0x38f24a24 => 58
	i32 956575887, ; 118: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 249
	i32 957807352, ; 119: Plugin.CurrentActivity => 0x3916faf8 => 65
	i32 958213972, ; 120: Xamarin.Android.Support.Media.Compat => 0x391d2f54 => 156
	i32 961995525, ; 121: Square.OkIO.dll => 0x3956e305 => 84
	i32 967690846, ; 122: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 194
	i32 974778368, ; 123: FormsViewGroup.dll => 0x3a19f000 => 23
	i32 975236339, ; 124: System.Diagnostics.Tracing => 0x3a20ecf3 => 275
	i32 987342438, ; 125: Xamarin.Android.Arch.Lifecycle.LiveData.dll => 0x3ad9a666 => 137
	i32 992768348, ; 126: System.Collections.dll => 0x3b2c715c => 3
	i32 1012816738, ; 127: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 216
	i32 1028951442, ; 128: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 48
	i32 1031141475, ; 129: Microsoft.AppCenter.Analytics => 0x3d75f863 => 33
	i32 1035644815, ; 130: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 169
	i32 1042160112, ; 131: Xamarin.Forms.Platform.dll => 0x3e1e19f0 => 237
	i32 1052210849, ; 132: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 200
	i32 1055189774, ; 133: Shiny.Core.dll => 0x3ee4eb0e => 72
	i32 1058641855, ; 134: Microsoft.AspNetCore.Http.Connections.Common => 0x3f1997bf => 40
	i32 1066173877, ; 135: Microsoft.AppCenter => 0x3f8c85b5 => 37
	i32 1084122840, ; 136: Xamarin.Kotlin.StdLib => 0x409e66d8 => 247
	i32 1098167829, ; 137: Xamarin.Android.Arch.Lifecycle.ViewModel => 0x4174b615 => 139
	i32 1098259244, ; 138: System => 0x41761b2c => 119
	i32 1104002344, ; 139: Plugin.Media => 0x41cdbd28 => 68
	i32 1134191450, ; 140: ZXingNetMobile.dll => 0x439a635a => 258
	i32 1137654822, ; 141: Plugin.Permissions.dll => 0x43cf3c26 => 69
	i32 1175144683, ; 142: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 224
	i32 1178241025, ; 143: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 207
	i32 1204270330, ; 144: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 170
	i32 1233093933, ; 145: Microsoft.AspNetCore.SignalR.Client.Core.dll => 0x497f852d => 42
	i32 1246590750, ; 146: GeoTimeZone.dll => 0x4a4d771e => 24
	i32 1264511973, ; 147: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 220
	i32 1267360935, ; 148: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 225
	i32 1275534314, ; 149: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 250
	i32 1292207520, ; 150: SQLitePCLRaw.core.dll => 0x4d0585a0 => 81
	i32 1292763917, ; 151: Xamarin.Android.Support.CursorAdapter.dll => 0x4d0e030d => 148
	i32 1293217323, ; 152: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 185
	i32 1297413738, ; 153: Xamarin.Android.Arch.Lifecycle.LiveData.Core => 0x4d54f66a => 136
	i32 1324164729, ; 154: System.Linq => 0x4eed2679 => 263
	i32 1364015309, ; 155: System.IO => 0x514d38cd => 264
	i32 1365406463, ; 156: System.ServiceModel.Internals.dll => 0x516272ff => 260
	i32 1376866003, ; 157: Xamarin.AndroidX.SavedState => 0x52114ed3 => 216
	i32 1379779777, ; 158: System.Resources.ResourceManager => 0x523dc4c1 => 2
	i32 1395857551, ; 159: Xamarin.AndroidX.Media.dll => 0x5333188f => 204
	i32 1406073936, ; 160: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 179
	i32 1411638395, ; 161: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 124
	i32 1414043276, ; 162: Microsoft.AspNetCore.Connections.Abstractions.dll => 0x5448968c => 38
	i32 1445445088, ; 163: Xamarin.Android.Support.Fragment => 0x5627bde0 => 152
	i32 1452756204, ; 164: Syncfusion.Expander.XForms.Android.dll => 0x56974cec => 92
	i32 1457743152, ; 165: System.Runtime.Extensions.dll => 0x56e36530 => 7
	i32 1460219004, ; 166: Xamarin.Forms.Xaml => 0x57092c7c => 238
	i32 1462112819, ; 167: System.IO.Compression.dll => 0x57261233 => 270
	i32 1469204771, ; 168: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 168
	i32 1470490898, ; 169: Microsoft.Extensions.Primitives => 0x57a5e912 => 53
	i32 1486716862, ; 170: Syncfusion.Cards.XForms => 0x589d7fbe => 88
	i32 1489278563, ; 171: Syncfusion.SfAutoComplete.XForms.dll => 0x58c49663 => 97
	i32 1516315406, ; 172: Syncfusion.Core.XForms.Android.dll => 0x5a61230e => 89
	i32 1520711082, ; 173: Syncfusion.SfChart.XForms.Android.dll => 0x5aa435aa => 101
	i32 1530663695, ; 174: Xamarin.Forms.Maps.dll => 0x5b3c130f => 235
	i32 1530772511, ; 175: FFImageLoading.Forms.Platform => 0x5b3dbc1f => 21
	i32 1533253840, ; 176: Syncfusion.SfBusyIndicator.Android.dll => 0x5b6398d0 => 98
	i32 1543031311, ; 177: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 266
	i32 1550322496, ; 178: System.Reflection.Extensions.dll => 0x5c680b40 => 8
	i32 1566488712, ; 179: Syncfusion.SfBusyIndicator.XForms.dll => 0x5d5eb888 => 100
	i32 1571005899, ; 180: zxing.portable => 0x5da3a5cb => 257
	i32 1574652163, ; 181: Xamarin.Android.Support.Core.Utils.dll => 0x5ddb4903 => 147
	i32 1582372066, ; 182: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 184
	i32 1582526884, ; 183: Microcharts.Forms.dll => 0x5e5371a4 => 31
	i32 1587447679, ; 184: Xamarin.Android.Arch.Core.Common.dll => 0x5e9e877f => 133
	i32 1592978981, ; 185: System.Runtime.Serialization.dll => 0x5ef2ee25 => 15
	i32 1622152042, ; 186: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 202
	i32 1624863272, ; 187: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 228
	i32 1634619362, ; 188: Xamarin.AndroidX.Room.Common => 0x616e4fe2 => 214
	i32 1635184631, ; 189: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 188
	i32 1636350590, ; 190: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 182
	i32 1639515021, ; 191: System.Net.Http.dll => 0x61b9038d => 14
	i32 1639986890, ; 192: System.Text.RegularExpressions => 0x61c036ca => 266
	i32 1653276212, ; 193: Microsoft.AppCenter.Android.Bindings => 0x628afe34 => 34
	i32 1657153582, ; 194: System.Runtime => 0x62c6282e => 125
	i32 1658241508, ; 195: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 222
	i32 1658251792, ; 196: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 239
	i32 1662014763, ; 197: Xamarin.Android.Support.Vector.Drawable => 0x6310552b => 162
	i32 1670060433, ; 198: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 178
	i32 1677501392, ; 199: System.Net.Primitives.dll => 0x63fca3d0 => 265
	i32 1698840827, ; 200: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 246
	i32 1700768604, ; 201: Retail => 0x655fab5c => 70
	i32 1701541528, ; 202: System.Diagnostics.Debug.dll => 0x656b7698 => 5
	i32 1711441057, ; 203: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 82
	i32 1712766919, ; 204: Syncfusion.SfComboBox.XForms.dll => 0x6616bfc7 => 104
	i32 1722051300, ; 205: SkiaSharp.Views.Forms => 0x66a46ae4 => 78
	i32 1726116996, ; 206: System.Reflection.dll => 0x66e27484 => 4
	i32 1729485958, ; 207: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 174
	i32 1736948048, ; 208: Xamarin.AndroidX.Sqlite.dll => 0x6787b950 => 218
	i32 1746115085, ; 209: System.IO.Pipelines.dll => 0x68139a0d => 120
	i32 1746316138, ; 210: Mono.Android.Export => 0x6816ab6a => 55
	i32 1758026047, ; 211: Xamarin.AndroidX.Room.Runtime.dll => 0x68c9593f => 215
	i32 1765942094, ; 212: System.Reflection.Extensions => 0x6942234e => 8
	i32 1766324549, ; 213: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 221
	i32 1770582343, ; 214: Microsoft.Extensions.Logging.dll => 0x6988f147 => 51
	i32 1776026572, ; 215: System.Core.dll => 0x69dc03cc => 117
	i32 1788241197, ; 216: Xamarin.AndroidX.Fragment => 0x6a96652d => 189
	i32 1793089559, ; 217: FFImageLoading.Forms.dll => 0x6ae06017 => 20
	i32 1796167890, ; 218: Microsoft.Bcl.AsyncInterfaces.dll => 0x6b0f58d2 => 46
	i32 1808609942, ; 219: Xamarin.AndroidX.Loader => 0x6bcd3296 => 202
	i32 1812481981, ; 220: Xamarin.Plugin.Calendar.dll => 0x6c0847bd => 252
	i32 1813058853, ; 221: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 247
	i32 1813201214, ; 222: Xamarin.Google.Android.Material => 0x6c13413e => 239
	i32 1818569960, ; 223: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 208
	i32 1819327070, ; 224: Microsoft.AspNetCore.Http.Features.dll => 0x6c70ba5e => 41
	i32 1828688058, ; 225: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 50
	i32 1840964413, ; 226: FFImageLoading.Forms => 0x6dbae33d => 20
	i32 1866717392, ; 227: Xamarin.Android.Support.Interpolator.dll => 0x6f43d8d0 => 153
	i32 1867746548, ; 228: Xamarin.Essentials.dll => 0x6f538cf4 => 231
	i32 1878053835, ; 229: Xamarin.Forms.Xaml.dll => 0x6ff0d3cb => 238
	i32 1881744805, ; 230: Shiny.Notifications.dll => 0x702925a5 => 73
	i32 1881862856, ; 231: Xamarin.Forms.Maps.Android.dll => 0x702af2c8 => 234
	i32 1885316902, ; 232: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 171
	i32 1900610850, ; 233: System.Resources.ResourceManager.dll => 0x71490522 => 2
	i32 1904184254, ; 234: FastAndroidCamera => 0x717f8bbe => 18
	i32 1904755420, ; 235: System.Runtime.InteropServices.WindowsRuntime.dll => 0x718842dc => 13
	i32 1908813208, ; 236: Xamarin.GooglePlayServices.Basement => 0x71c62d98 => 242
	i32 1916660109, ; 237: Xamarin.Android.Arch.Lifecycle.ViewModel.dll => 0x723de98d => 139
	i32 1919157823, ; 238: Xamarin.AndroidX.MultiDex.dll => 0x7264063f => 205
	i32 1936121326, ; 239: Syncfusion.SfPicker.XForms.dll => 0x7366ddee => 112
	i32 1945717188, ; 240: Microsoft.AspNetCore.SignalR.Client.Core => 0x73f949c4 => 42
	i32 1967334205, ; 241: Microsoft.AspNetCore.SignalR.Common => 0x7543233d => 44
	i32 1983156543, ; 242: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 246
	i32 1991544456, ; 243: SignaturePad.Forms.dll => 0x76b48e88 => 75
	i32 2011961780, ; 244: System.Buffers.dll => 0x77ec19b4 => 116
	i32 2019465201, ; 245: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 200
	i32 2024078044, ; 246: Microsoft.AppCenter.Analytics.dll => 0x78a4fadc => 33
	i32 2036898326, ; 247: Plugin.Iconize.dll => 0x79689a16 => 67
	i32 2037417872, ; 248: Xamarin.Android.Support.ViewPager => 0x79708790 => 164
	i32 2044222327, ; 249: Xamarin.Android.Support.Loader => 0x79d85b77 => 154
	i32 2045425888, ; 250: Microsoft.AppCenter.Analytics.Android.Bindings => 0x79eab8e0 => 32
	i32 2048185678, ; 251: Plugin.Media.dll => 0x7a14d54e => 68
	i32 2055257422, ; 252: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 195
	i32 2057001415, ; 253: Retail.Android.dll => 0x7a9b59c7 => 0
	i32 2071563619, ; 254: Syncfusion.Buttons.XForms.Android => 0x7b798d63 => 85
	i32 2079903147, ; 255: System.Runtime.dll => 0x7bf8cdab => 125
	i32 2090596640, ; 256: System.Numerics.Vectors => 0x7c9bf920 => 122
	i32 2097448633, ; 257: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x7d0486b9 => 191
	i32 2103459038, ; 258: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 83
	i32 2126786730, ; 259: Xamarin.Forms.Platform.Android => 0x7ec430aa => 236
	i32 2129483829, ; 260: Xamarin.GooglePlayServices.Base.dll => 0x7eed5835 => 241
	i32 2133113917, ; 261: Syncfusion.SfChart.XForms => 0x7f24bc3d => 102
	i32 2139458754, ; 262: Xamarin.Android.Support.DrawerLayout => 0x7f858cc2 => 151
	i32 2153606111, ; 263: Retail.Android => 0x805d6bdf => 0
	i32 2166116741, ; 264: Xamarin.Android.Support.Core.Utils => 0x811c5185 => 147
	i32 2174120593, ; 265: Syncfusion.SfRadialMenu.XForms.Android => 0x81967291 => 114
	i32 2181898931, ; 266: Microsoft.Extensions.Options.dll => 0x820d22b3 => 52
	i32 2192057212, ; 267: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 50
	i32 2193016926, ; 268: System.ObjectModel.dll => 0x82b6c85e => 261
	i32 2195767025, ; 269: Syncfusion.SfDataGrid.XForms.Android.dll => 0x82e0bef1 => 105
	i32 2196165013, ; 270: Xamarin.Android.Support.VersionedParcelable => 0x82e6d195 => 163
	i32 2201107256, ; 271: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 251
	i32 2201231467, ; 272: System.Net.Http => 0x8334206b => 14
	i32 2217644978, ; 273: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 224
	i32 2244775296, ; 274: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 203
	i32 2256548716, ; 275: Xamarin.AndroidX.MultiDex => 0x8680336c => 205
	i32 2261435625, ; 276: Xamarin.AndroidX.Legacy.Support.V4.dll => 0x86cac4e9 => 193
	i32 2272153315, ; 277: Syncfusion.SfPicker.Android.dll => 0x876e4ee3 => 110
	i32 2279703579, ; 278: Xamarin.AndroidX.Sqlite.Framework.dll => 0x87e1841b => 219
	i32 2279755925, ; 279: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 212
	i32 2292652185, ; 280: Syncfusion.SfRadialMenu.Android.dll => 0x88a71899 => 113
	i32 2306217607, ; 281: OxyPlot.Xamarin.Forms => 0x89761687 => 63
	i32 2308925481, ; 282: Octane.Xamarin.Forms.VideoPlayer.dll => 0x899f6829 => 60
	i32 2315684594, ; 283: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 166
	i32 2319144366, ; 284: Microsoft.AspNetCore.SignalR.Client => 0x8a3b55ae => 43
	i32 2324593299, ; 285: Syncfusion.SfDataGrid.XForms => 0x8a8e7a93 => 106
	i32 2329204181, ; 286: zxing.portable.dll => 0x8ad4d5d5 => 257
	i32 2330457430, ; 287: Xamarin.Android.Support.Core.UI.dll => 0x8ae7f556 => 146
	i32 2330986192, ; 288: Xamarin.Android.Support.SlidingPaneLayout.dll => 0x8af006d0 => 158
	i32 2340826669, ; 289: FFImageLoading.dll => 0x8b862e2d => 19
	i32 2341995103, ; 290: ZXingNetMobile => 0x8b98025f => 258
	i32 2343171156, ; 291: Syncfusion.Core.XForms => 0x8ba9f454 => 90
	i32 2353062107, ; 292: System.Net.Primitives => 0x8c40e0db => 265
	i32 2354730003, ; 293: Syncfusion.Licensing => 0x8c5a5413 => 95
	i32 2368005991, ; 294: System.Xml.ReaderWriter.dll => 0x8d24e767 => 274
	i32 2373288475, ; 295: Xamarin.Android.Support.Fragment.dll => 0x8d75821b => 152
	i32 2403452196, ; 296: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 187
	i32 2409053734, ; 297: Xamarin.AndroidX.Preference.dll => 0x8f973e26 => 209
	i32 2431243866, ; 298: ZXing.Net.Mobile.Core.dll => 0x90e9d65a => 254
	i32 2440966767, ; 299: Xamarin.Android.Support.Loader.dll => 0x917e326f => 154
	i32 2454642406, ; 300: System.Text.Encoding.dll => 0x924edee6 => 9
	i32 2465273461, ; 301: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 80
	i32 2465532216, ; 302: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 177
	i32 2471215200, ; 303: ImageCircle.Forms.Plugin => 0x934bc060 => 25
	i32 2471841756, ; 304: netstandard.dll => 0x93554fdc => 57
	i32 2475788418, ; 305: Java.Interop.dll => 0x93918882 => 26
	i32 2482213323, ; 306: ZXing.Net.Mobile.Forms.Android => 0x93f391cb => 255
	i32 2487632829, ; 307: Xamarin.Android.Support.DocumentFile => 0x944643bd => 150
	i32 2490993605, ; 308: System.AppContext.dll => 0x94798bc5 => 277
	i32 2501346920, ; 309: System.Data.DataSetExtensions => 0x95178668 => 269
	i32 2505896520, ; 310: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 198
	i32 2515901310, ; 311: Retail.dll => 0x95f59b7e => 70
	i32 2562349572, ; 312: Microsoft.CSharp => 0x98ba5a04 => 47
	i32 2563143864, ; 313: AndHUD.dll => 0x98c678b8 => 16
	i32 2568748418, ; 314: OxyPlot.Xamarin.Forms.Platform.Android => 0x991bfd82 => 64
	i32 2570120770, ; 315: System.Text.Encodings.Web => 0x9930ee42 => 126
	i32 2581819634, ; 316: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 225
	i32 2605712449, ; 317: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 251
	i32 2620871830, ; 318: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 182
	i32 2624644809, ; 319: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 186
	i32 2624721879, ; 320: Syncfusion.SfChart.XForms.Android => 0x9c7213d7 => 101
	i32 2633051222, ; 321: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 196
	i32 2635217119, ; 322: Syncfusion.SfAutoComplete.XForms.Android => 0x9d1238df => 96
	i32 2635300604, ; 323: Syncfusion.Buttons.XForms => 0x9d137efc => 86
	i32 2647358571, ; 324: Syncfusion.SfAutoComplete.XForms.Android.dll => 0x9dcb7c6b => 96
	i32 2689529426, ; 325: OxyPlot.Xamarin.Android => 0xa04ef652 => 62
	i32 2693849962, ; 326: System.IO.dll => 0xa090e36a => 264
	i32 2697269578, ; 327: Microsoft.AppCenter.Crashes.dll => 0xa0c5114a => 36
	i32 2698266930, ; 328: Xamarin.Android.Arch.Lifecycle.LiveData => 0xa0d44932 => 137
	i32 2701096212, ; 329: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 222
	i32 2715334215, ; 330: System.Threading.Tasks.dll => 0xa1d8b647 => 262
	i32 2732626843, ; 331: Xamarin.AndroidX.Activity => 0xa2e0939b => 165
	i32 2735172069, ; 332: System.Threading.Channels => 0xa30769e5 => 128
	i32 2737747696, ; 333: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 168
	i32 2739926663, ; 334: Xamarin.AndroidX.Sqlite.Framework => 0xa34ff687 => 219
	i32 2747619038, ; 335: OxyPlot.Xamarin.Android.dll => 0xa3c556de => 62
	i32 2751899777, ; 336: Xamarin.Android.Support.Collections => 0xa406a881 => 143
	i32 2766581644, ; 337: Xamarin.Forms.Core => 0xa4e6af8c => 232
	i32 2770495804, ; 338: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 245
	i32 2778768386, ; 339: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 227
	i32 2779977773, ; 340: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 213
	i32 2784016111, ; 341: Syncfusion.SfPicker.XForms => 0xa5f0b6ef => 112
	i32 2788639665, ; 342: Xamarin.Android.Support.LocalBroadcastManager => 0xa63743b1 => 155
	i32 2788775637, ; 343: Xamarin.Android.Support.SwipeRefreshLayout.dll => 0xa63956d5 => 159
	i32 2795602088, ; 344: SkiaSharp.Views.Android.dll => 0xa6a180a8 => 77
	i32 2806986428, ; 345: Plugin.CurrentActivity.dll => 0xa74f36bc => 65
	i32 2810250172, ; 346: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 179
	i32 2819470561, ; 347: System.Xml.dll => 0xa80db4e1 => 129
	i32 2821294376, ; 348: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 213
	i32 2842369275, ; 349: FFImageLoading.Forms.Platform.dll => 0xa96b1cfb => 21
	i32 2843355708, ; 350: Lottie.Android.dll => 0xa97a2a3c => 27
	i32 2847418871, ; 351: Xamarin.GooglePlayServices.Base => 0xa9b829f7 => 241
	i32 2849962309, ; 352: Microsoft.AppCenter.Android.Bindings.dll => 0xa9def945 => 34
	i32 2850549256, ; 353: Microsoft.AspNetCore.Http.Features => 0xa9e7ee08 => 41
	i32 2853208004, ; 354: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 227
	i32 2855708567, ; 355: Xamarin.AndroidX.Transition => 0xaa36a797 => 223
	i32 2861098320, ; 356: Mono.Android.Export.dll => 0xaa88e550 => 55
	i32 2861816565, ; 357: Rg.Plugins.Popup.dll => 0xaa93daf5 => 71
	i32 2868557005, ; 358: Syncfusion.Licensing.dll => 0xaafab4cd => 95
	i32 2873222696, ; 359: FFImageLoading => 0xab41e628 => 19
	i32 2874148507, ; 360: Syncfusion.Core.XForms.Android => 0xab50069b => 89
	i32 2875347124, ; 361: Microsoft.AspNetCore.Http.Connections.Client.dll => 0xab6250b4 => 39
	i32 2876493027, ; 362: Xamarin.Android.Support.SwipeRefreshLayout => 0xab73cce3 => 159
	i32 2886109683, ; 363: Syncfusion.SfBusyIndicator.XForms.Android.dll => 0xac0689f3 => 99
	i32 2893257763, ; 364: Xamarin.Android.Arch.Core.Runtime.dll => 0xac739c23 => 134
	i32 2901442782, ; 365: System.Reflection => 0xacf080de => 4
	i32 2903344695, ; 366: System.ComponentModel.Composition => 0xad0d8637 => 272
	i32 2905242038, ; 367: mscorlib.dll => 0xad2a79b6 => 56
	i32 2912489636, ; 368: SkiaSharp.Views.Android => 0xad9910a4 => 77
	i32 2916838712, ; 369: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 228
	i32 2919462931, ; 370: System.Numerics.Vectors.dll => 0xae037813 => 122
	i32 2921128767, ; 371: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 167
	i32 2921692953, ; 372: Xamarin.Android.Support.CustomView.dll => 0xae257f19 => 149
	i32 2922925221, ; 373: Xamarin.Android.Support.Vector.Drawable.dll => 0xae384ca5 => 162
	i32 2943219317, ; 374: Square.OkIO => 0xaf6df675 => 84
	i32 2944473000, ; 375: Octane.Xamarin.Forms.VideoPlayer.Android => 0xaf8117a8 => 59
	i32 2953735189, ; 376: Xamarin.AndroidX.Lifecycle.Service.dll => 0xb00e6c15 => 199
	i32 2969700472, ; 377: Syncfusion.Data.Portable => 0xb1020878 => 91
	i32 2974793899, ; 378: SkiaSharp.Views.Forms.dll => 0xb14fc0ab => 78
	i32 2978675010, ; 379: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 185
	i32 2986342291, ; 380: Xamanimation => 0xb1fff793 => 132
	i32 2996846495, ; 381: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 197
	i32 3016983068, ; 382: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 220
	i32 3017076677, ; 383: Xamarin.GooglePlayServices.Maps => 0xb3d4efc5 => 243
	i32 3021783730, ; 384: Octane.Xamarin.Forms.VideoPlayer.Android.dll => 0xb41cc2b2 => 59
	i32 3024354802, ; 385: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 192
	i32 3036068679, ; 386: Microcharts.Droid.dll => 0xb4f6bb47 => 30
	i32 3037436946, ; 387: Microsoft.AppCenter.Analytics.Android.Bindings.dll => 0xb50b9c12 => 32
	i32 3044182254, ; 388: FormsViewGroup => 0xb57288ee => 23
	i32 3048527850, ; 389: Syncfusion.SfNumericTextBox.Android.dll => 0xb5b4d7ea => 107
	i32 3056250942, ; 390: Xamarin.Android.Support.AsyncLayoutInflater.dll => 0xb62ab03e => 142
	i32 3057625584, ; 391: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 206
	i32 3058099980, ; 392: Xamarin.GooglePlayServices.Tasks => 0xb646e70c => 244
	i32 3068715062, ; 393: Xamarin.Android.Arch.Lifecycle.Common => 0xb6e8e036 => 135
	i32 3075834255, ; 394: System.Threading.Tasks => 0xb755818f => 262
	i32 3085392760, ; 395: OxyPlot => 0xb7e75b78 => 61
	i32 3092211740, ; 396: Xamarin.Android.Support.Media.Compat.dll => 0xb84f681c => 156
	i32 3104361006, ; 397: DLToolkit.Forms.Controls.FlowListView.dll => 0xb908ca2e => 17
	i32 3111772706, ; 398: System.Runtime.Serialization => 0xb979e222 => 15
	i32 3124832203, ; 399: System.Threading.Tasks.Extensions => 0xba4127cb => 279
	i32 3144132135, ; 400: Xamarin.AndroidX.Sqlite => 0xbb67a627 => 218
	i32 3147165239, ; 401: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 275
	i32 3147431871, ; 402: OxyPlot.Xamarin.Forms.dll => 0xbb99ffbf => 63
	i32 3204380047, ; 403: System.Data.dll => 0xbefef58f => 11
	i32 3204912593, ; 404: Xamarin.Android.Support.AsyncLayoutInflater => 0xbf0715d1 => 142
	i32 3209966310, ; 405: Plugin.DeviceInfo.dll => 0xbf5432e6 => 66
	i32 3211777861, ; 406: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 184
	i32 3220365878, ; 407: System.Threading => 0xbff2e236 => 6
	i32 3223546065, ; 408: Syncfusion.Expander.XForms => 0xc02368d1 => 93
	i32 3225527422, ; 409: GeoTimeZone => 0xc041a47e => 24
	i32 3230466174, ; 410: Xamarin.GooglePlayServices.Basement.dll => 0xc08d007e => 242
	i32 3233339011, ; 411: Xamarin.Android.Arch.Lifecycle.LiveData.Core.dll => 0xc0b8d683 => 136
	i32 3238542748, ; 412: Syncfusion.SfDataGrid.XForms.Android => 0xc1083d9c => 105
	i32 3247949154, ; 413: Mono.Security => 0xc197c562 => 278
	i32 3258312781, ; 414: Xamarin.AndroidX.CardView => 0xc235e84d => 174
	i32 3263631797, ; 415: Lottie.Forms => 0xc28711b5 => 28
	i32 3264650703, ; 416: XF.Material => 0xc2969dcf => 253
	i32 3265893370, ; 417: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 279
	i32 3267021929, ; 418: Xamarin.AndroidX.AsyncLayoutInflater => 0xc2bacc69 => 172
	i32 3270722617, ; 419: Syncfusion.SfPicker.Android => 0xc2f34439 => 110
	i32 3286872994, ; 420: SQLite-net.dll => 0xc3e9b3a2 => 79
	i32 3296380511, ; 421: Xamarin.Android.Support.Collections.dll => 0xc47ac65f => 143
	i32 3299363146, ; 422: System.Text.Encoding => 0xc4a8494a => 9
	i32 3317135071, ; 423: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 183
	i32 3317144872, ; 424: System.Data => 0xc5b79d28 => 11
	i32 3321585405, ; 425: Xamarin.Android.Support.DocumentFile.dll => 0xc5fb5efd => 150
	i32 3329003366, ; 426: Syncfusion.SfNumericTextBox.XForms.Android.dll => 0xc66c8f66 => 108
	i32 3340387945, ; 427: SkiaSharp => 0xc71a4669 => 76
	i32 3340431453, ; 428: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 171
	i32 3345895724, ; 429: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 211
	i32 3346324047, ; 430: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 207
	i32 3352662376, ; 431: Xamarin.Android.Support.CoordinaterLayout => 0xc7d59168 => 145
	i32 3353484488, ; 432: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0xc7e21cc8 => 191
	i32 3353544232, ; 433: Xamarin.CommunityToolkit.dll => 0xc7e30628 => 230
	i32 3357663996, ; 434: Xamarin.Android.Support.CursorAdapter => 0xc821e2fc => 148
	i32 3358260929, ; 435: System.Text.Json => 0xc82afec1 => 127
	i32 3360279109, ; 436: SQLitePCLRaw.core => 0xc849ca45 => 81
	i32 3362522851, ; 437: Xamarin.AndroidX.Core => 0xc86c06e3 => 181
	i32 3366347497, ; 438: Java.Interop => 0xc8a662e9 => 26
	i32 3369296695, ; 439: Syncfusion.SfRadialMenu.XForms => 0xc8d36337 => 115
	i32 3374999561, ; 440: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 212
	i32 3395150330, ; 441: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 124
	i32 3404865022, ; 442: System.ServiceModel.Internals => 0xcaf21dfe => 260
	i32 3407215217, ; 443: Xamarin.CommunityToolkit => 0xcb15fa71 => 230
	i32 3428513518, ; 444: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 49
	i32 3429136800, ; 445: System.Xml => 0xcc6479a0 => 129
	i32 3430777524, ; 446: netstandard => 0xcc7d82b4 => 57
	i32 3434749838, ; 447: Syncfusion.Buttons.XForms.Android.dll => 0xccba1f8e => 85
	i32 3439690031, ; 448: Xamarin.Android.Support.Annotations.dll => 0xcd05812f => 141
	i32 3441283291, ; 449: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 186
	i32 3442543374, ; 450: AndHUD => 0xcd310b0e => 16
	i32 3448958507, ; 451: Syncfusion.GridCommon.Portable.dll => 0xcd92ee2b => 94
	i32 3455791806, ; 452: Microcharts => 0xcdfb32be => 29
	i32 3460255358, ; 453: Syncfusion.SfBusyIndicator.XForms => 0xce3f4e7e => 100
	i32 3466904072, ; 454: Microsoft.AspNetCore.SignalR.Client.dll => 0xcea4c208 => 43
	i32 3476120550, ; 455: Mono.Android => 0xcf3163e6 => 54
	i32 3483112796, ; 456: ImageCircle.Forms.Plugin.dll => 0xcf9c155c => 25
	i32 3485117614, ; 457: System.Text.Json.dll => 0xcfbaacae => 127
	i32 3486566296, ; 458: System.Transactions => 0xcfd0c798 => 259
	i32 3493954962, ; 459: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 176
	i32 3499824017, ; 460: Syncfusion.Expander.XForms.dll => 0xd09b1391 => 93
	i32 3501239056, ; 461: Xamarin.AndroidX.AsyncLayoutInflater.dll => 0xd0b0ab10 => 172
	i32 3509114376, ; 462: System.Xml.Linq => 0xd128d608 => 130
	i32 3536029504, ; 463: Xamarin.Forms.Platform.Android.dll => 0xd2c38740 => 236
	i32 3547625832, ; 464: Xamarin.Android.Support.SlidingPaneLayout => 0xd3747968 => 158
	i32 3567349600, ; 465: System.ComponentModel.Composition.dll => 0xd4a16f60 => 272
	i32 3608519521, ; 466: System.Linq.dll => 0xd715a361 => 263
	i32 3612305132, ; 467: Syncfusion.SfDataGrid.XForms.dll => 0xd74f66ec => 106
	i32 3612947231, ; 468: Xamarin.AndroidX.Work.Runtime => 0xd759331f => 229
	i32 3618140916, ; 469: Xamarin.AndroidX.Preference => 0xd7a872f4 => 209
	i32 3627220390, ; 470: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 210
	i32 3632266177, ; 471: Syncfusion.SfComboBox.XForms => 0xd87ffbc1 => 104
	i32 3632359727, ; 472: Xamarin.Forms.Platform => 0xd881692f => 237
	i32 3633644679, ; 473: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 167
	i32 3639449509, ; 474: Lottie.Android => 0xd8ed97a5 => 27
	i32 3641597786, ; 475: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 195
	i32 3653605406, ; 476: TimeZoneConverter => 0xd9c5981e => 131
	i32 3664423555, ; 477: Xamarin.Android.Support.ViewPager.dll => 0xda6aaa83 => 164
	i32 3668042751, ; 478: Microcharts.dll => 0xdaa1e3ff => 29
	i32 3672681054, ; 479: Mono.Android.dll => 0xdae8aa5e => 54
	i32 3676310014, ; 480: System.Web.Services.dll => 0xdb2009fe => 273
	i32 3678221644, ; 481: Xamarin.Android.Support.v7.AppCompat => 0xdb3d354c => 161
	i32 3681174138, ; 482: Xamarin.Android.Arch.Lifecycle.Common.dll => 0xdb6a427a => 135
	i32 3682565725, ; 483: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 173
	i32 3684561358, ; 484: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 176
	i32 3684933406, ; 485: System.Runtime.InteropServices.WindowsRuntime => 0xdba39f1e => 13
	i32 3689375977, ; 486: System.Drawing.Common => 0xdbe768e9 => 10
	i32 3691870036, ; 487: Microsoft.AspNetCore.SignalR.Protocols.Json => 0xdc0d7754 => 45
	i32 3693477420, ; 488: Syncfusion.SfNumericTextBox.XForms => 0xdc25fe2c => 109
	i32 3706696989, ; 489: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 180
	i32 3714038699, ; 490: Xamarin.Android.Support.LocalBroadcastManager.dll => 0xdd5fbbab => 155
	i32 3718463572, ; 491: Xamarin.Android.Support.Animated.Vector.Drawable.dll => 0xdda34054 => 140
	i32 3718780102, ; 492: Xamarin.AndroidX.Annotation => 0xdda814c6 => 166
	i32 3724971120, ; 493: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 206
	i32 3731644420, ; 494: System.Reactive => 0xde6c6004 => 123
	i32 3735092365, ; 495: Xamarin.AndroidX.Room.Common.dll => 0xdea0fc8d => 214
	i32 3748608112, ; 496: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 118
	i32 3754567612, ; 497: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 83
	i32 3758932259, ; 498: Xamarin.AndroidX.Legacy.Support.V4 => 0xe00cc123 => 193
	i32 3767618864, ; 499: Xamarin.Forms.DebugRainbows => 0xe0914d30 => 233
	i32 3776062606, ; 500: Xamarin.Android.Support.DrawerLayout.dll => 0xe112248e => 151
	i32 3786282454, ; 501: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 175
	i32 3787005001, ; 502: Microsoft.AspNetCore.Connections.Abstractions => 0xe1b91c49 => 38
	i32 3822602673, ; 503: Xamarin.AndroidX.Media => 0xe3d849b1 => 204
	i32 3829621856, ; 504: System.Numerics.dll => 0xe4436460 => 121
	i32 3832731800, ; 505: Xamarin.Android.Support.CoordinaterLayout.dll => 0xe472d898 => 145
	i32 3841636137, ; 506: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 48
	i32 3847036339, ; 507: ZXing.Net.Mobile.Forms.Android.dll => 0xe54d1db3 => 255
	i32 3849253459, ; 508: System.Runtime.InteropServices.dll => 0xe56ef253 => 276
	i32 3854864648, ; 509: OxyPlot.Xamarin.Forms.Platform.Android.dll => 0xe5c49108 => 64
	i32 3862817207, ; 510: Xamarin.Android.Arch.Lifecycle.Runtime.dll => 0xe63de9b7 => 138
	i32 3866512743, ; 511: Syncfusion.SfRadialMenu.XForms.dll => 0xe6764d67 => 115
	i32 3869245228, ; 512: Microsoft.AppCenter.Crashes.Android.Bindings.dll => 0xe69fff2c => 35
	i32 3874897629, ; 513: Xamarin.Android.Arch.Lifecycle.Runtime => 0xe6f63edd => 138
	i32 3876362041, ; 514: SQLite-net => 0xe70c9739 => 79
	i32 3883175360, ; 515: Xamarin.Android.Support.v7.AppCompat.dll => 0xe7748dc0 => 161
	i32 3885922214, ; 516: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 223
	i32 3888767677, ; 517: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 211
	i32 3896106733, ; 518: System.Collections.Concurrent.dll => 0xe839deed => 12
	i32 3896760992, ; 519: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 181
	i32 3903721208, ; 520: Microcharts.Forms => 0xe8ae0ef8 => 31
	i32 3920810846, ; 521: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 271
	i32 3921031405, ; 522: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 226
	i32 3928044579, ; 523: System.Xml.ReaderWriter => 0xea213423 => 274
	i32 3929079551, ; 524: Syncfusion.Cards.XForms.Android.dll => 0xea30feff => 87
	i32 3931092270, ; 525: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 208
	i32 3945713374, ; 526: System.Data.DataSetExtensions.dll => 0xeb2ecede => 269
	i32 3949143839, ; 527: Syncfusion.SfPicker.XForms.Android.dll => 0xeb63271f => 111
	i32 3955647286, ; 528: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 169
	i32 3959773229, ; 529: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 197
	i32 3965022950, ; 530: Syncfusion.SfComboBox.XForms.Android.dll => 0xec5572e6 => 103
	i32 3970018735, ; 531: Xamarin.GooglePlayServices.Tasks.dll => 0xeca1adaf => 244
	i32 4004095362, ; 532: Syncfusion.Expander.XForms.Android => 0xeea9a582 => 92
	i32 4023392905, ; 533: System.IO.Pipelines => 0xefd01a89 => 120
	i32 4025784931, ; 534: System.Memory => 0xeff49a63 => 268
	i32 4047670059, ; 535: Xamanimation.dll => 0xf1428b2b => 132
	i32 4051712506, ; 536: Syncfusion.GridCommon.Portable => 0xf18039fa => 94
	i32 4063435586, ; 537: Xamarin.Android.Support.CustomView => 0xf2331b42 => 149
	i32 4071430779, ; 538: SignaturePad.Forms => 0xf2ad1a7b => 75
	i32 4073602200, ; 539: System.Threading.dll => 0xf2ce3c98 => 6
	i32 4078019152, ; 540: Plugin.Iconize => 0xf311a250 => 67
	i32 4090812903, ; 541: Syncfusion.SfNumericTextBox.Android => 0xf3d4d9e7 => 107
	i32 4101593132, ; 542: Xamarin.AndroidX.Emoji2 => 0xf479582c => 187
	i32 4105002889, ; 543: Mono.Security.dll => 0xf4ad5f89 => 278
	i32 4118017053, ; 544: Syncfusion.SfNumericTextBox.XForms.dll => 0xf573f41d => 109
	i32 4126470640, ; 545: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 49
	i32 4130442656, ; 546: System.AppContext => 0xf6318da0 => 277
	i32 4137181845, ; 547: Xamarin.AndroidX.Room.Runtime => 0xf6986295 => 215
	i32 4146307099, ; 548: Microsoft.AppCenter.Crashes => 0xf723a01b => 36
	i32 4151237749, ; 549: System.Core => 0xf76edc75 => 117
	i32 4182413190, ; 550: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 201
	i32 4184283386, ; 551: FFImageLoading.Platform.dll => 0xf96718fa => 22
	i32 4186595366, ; 552: ZXing.Net.Mobile.Core => 0xf98a6026 => 254
	i32 4213026141, ; 553: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 118
	i32 4249538387, ; 554: TimeZoneConverter.dll => 0xfd4acf53 => 131
	i32 4256097574, ; 555: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 180
	i32 4260525087, ; 556: System.Buffers => 0xfdf2741f => 116
	i32 4278134329, ; 557: Xamarin.GooglePlayServices.Maps.dll => 0xfeff2639 => 243
	i32 4287798392, ; 558: Shiny.Notifications => 0xff929c78 => 73
	i32 4292120959 ; 559: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 201
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [560 x i32] [
	i32 87, i32 113, i32 61, i32 198, i32 240, i32 58, i32 69, i32 232, ; 0..7
	i32 163, i32 108, i32 86, i32 217, i32 253, i32 276, i32 217, i32 128, ; 8..15
	i32 248, i32 28, i32 133, i32 175, i32 144, i32 18, i32 221, i32 60, ; 16..23
	i32 134, i32 173, i32 234, i32 160, i32 5, i32 39, i32 192, i32 47, ; 24..31
	i32 267, i32 273, i32 178, i32 196, i32 190, i32 102, i32 229, i32 165, ; 32..39
	i32 235, i32 121, i32 99, i32 256, i32 194, i32 82, i32 40, i32 97, ; 40..47
	i32 268, i32 140, i32 146, i32 252, i32 1, i32 177, i32 231, i32 3, ; 48..55
	i32 189, i32 44, i32 56, i32 119, i32 190, i32 203, i32 233, i32 261, ; 56..63
	i32 53, i32 144, i32 22, i32 157, i32 76, i32 259, i32 248, i32 98, ; 64..71
	i32 51, i32 1, i32 7, i32 46, i32 111, i32 271, i32 123, i32 183, ; 72..79
	i32 188, i32 126, i32 226, i32 170, i32 12, i32 130, i32 250, i32 141, ; 80..87
	i32 245, i32 88, i32 91, i32 90, i32 249, i32 37, i32 103, i32 72, ; 88..95
	i32 80, i32 270, i32 52, i32 17, i32 153, i32 74, i32 66, i32 10, ; 96..103
	i32 45, i32 199, i32 210, i32 267, i32 160, i32 30, i32 35, i32 71, ; 104..111
	i32 74, i32 157, i32 240, i32 256, i32 114, i32 58, i32 249, i32 65, ; 112..119
	i32 156, i32 84, i32 194, i32 23, i32 275, i32 137, i32 3, i32 216, ; 120..127
	i32 48, i32 33, i32 169, i32 237, i32 200, i32 72, i32 40, i32 37, ; 128..135
	i32 247, i32 139, i32 119, i32 68, i32 258, i32 69, i32 224, i32 207, ; 136..143
	i32 170, i32 42, i32 24, i32 220, i32 225, i32 250, i32 81, i32 148, ; 144..151
	i32 185, i32 136, i32 263, i32 264, i32 260, i32 216, i32 2, i32 204, ; 152..159
	i32 179, i32 124, i32 38, i32 152, i32 92, i32 7, i32 238, i32 270, ; 160..167
	i32 168, i32 53, i32 88, i32 97, i32 89, i32 101, i32 235, i32 21, ; 168..175
	i32 98, i32 266, i32 8, i32 100, i32 257, i32 147, i32 184, i32 31, ; 176..183
	i32 133, i32 15, i32 202, i32 228, i32 214, i32 188, i32 182, i32 14, ; 184..191
	i32 266, i32 34, i32 125, i32 222, i32 239, i32 162, i32 178, i32 265, ; 192..199
	i32 246, i32 70, i32 5, i32 82, i32 104, i32 78, i32 4, i32 174, ; 200..207
	i32 218, i32 120, i32 55, i32 215, i32 8, i32 221, i32 51, i32 117, ; 208..215
	i32 189, i32 20, i32 46, i32 202, i32 252, i32 247, i32 239, i32 208, ; 216..223
	i32 41, i32 50, i32 20, i32 153, i32 231, i32 238, i32 73, i32 234, ; 224..231
	i32 171, i32 2, i32 18, i32 13, i32 242, i32 139, i32 205, i32 112, ; 232..239
	i32 42, i32 44, i32 246, i32 75, i32 116, i32 200, i32 33, i32 67, ; 240..247
	i32 164, i32 154, i32 32, i32 68, i32 195, i32 0, i32 85, i32 125, ; 248..255
	i32 122, i32 191, i32 83, i32 236, i32 241, i32 102, i32 151, i32 0, ; 256..263
	i32 147, i32 114, i32 52, i32 50, i32 261, i32 105, i32 163, i32 251, ; 264..271
	i32 14, i32 224, i32 203, i32 205, i32 193, i32 110, i32 219, i32 212, ; 272..279
	i32 113, i32 63, i32 60, i32 166, i32 43, i32 106, i32 257, i32 146, ; 280..287
	i32 158, i32 19, i32 258, i32 90, i32 265, i32 95, i32 274, i32 152, ; 288..295
	i32 187, i32 209, i32 254, i32 154, i32 9, i32 80, i32 177, i32 25, ; 296..303
	i32 57, i32 26, i32 255, i32 150, i32 277, i32 269, i32 198, i32 70, ; 304..311
	i32 47, i32 16, i32 64, i32 126, i32 225, i32 251, i32 182, i32 186, ; 312..319
	i32 101, i32 196, i32 96, i32 86, i32 96, i32 62, i32 264, i32 36, ; 320..327
	i32 137, i32 222, i32 262, i32 165, i32 128, i32 168, i32 219, i32 62, ; 328..335
	i32 143, i32 232, i32 245, i32 227, i32 213, i32 112, i32 155, i32 159, ; 336..343
	i32 77, i32 65, i32 179, i32 129, i32 213, i32 21, i32 27, i32 241, ; 344..351
	i32 34, i32 41, i32 227, i32 223, i32 55, i32 71, i32 95, i32 19, ; 352..359
	i32 89, i32 39, i32 159, i32 99, i32 134, i32 4, i32 272, i32 56, ; 360..367
	i32 77, i32 228, i32 122, i32 167, i32 149, i32 162, i32 84, i32 59, ; 368..375
	i32 199, i32 91, i32 78, i32 185, i32 132, i32 197, i32 220, i32 243, ; 376..383
	i32 59, i32 192, i32 30, i32 32, i32 23, i32 107, i32 142, i32 206, ; 384..391
	i32 244, i32 135, i32 262, i32 61, i32 156, i32 17, i32 15, i32 279, ; 392..399
	i32 218, i32 275, i32 63, i32 11, i32 142, i32 66, i32 184, i32 6, ; 400..407
	i32 93, i32 24, i32 242, i32 136, i32 105, i32 278, i32 174, i32 28, ; 408..415
	i32 253, i32 279, i32 172, i32 110, i32 79, i32 143, i32 9, i32 183, ; 416..423
	i32 11, i32 150, i32 108, i32 76, i32 171, i32 211, i32 207, i32 145, ; 424..431
	i32 191, i32 230, i32 148, i32 127, i32 81, i32 181, i32 26, i32 115, ; 432..439
	i32 212, i32 124, i32 260, i32 230, i32 49, i32 129, i32 57, i32 85, ; 440..447
	i32 141, i32 186, i32 16, i32 94, i32 29, i32 100, i32 43, i32 54, ; 448..455
	i32 25, i32 127, i32 259, i32 176, i32 93, i32 172, i32 130, i32 236, ; 456..463
	i32 158, i32 272, i32 263, i32 106, i32 229, i32 209, i32 210, i32 104, ; 464..471
	i32 237, i32 167, i32 27, i32 195, i32 131, i32 164, i32 29, i32 54, ; 472..479
	i32 273, i32 161, i32 135, i32 173, i32 176, i32 13, i32 10, i32 45, ; 480..487
	i32 109, i32 180, i32 155, i32 140, i32 166, i32 206, i32 123, i32 214, ; 488..495
	i32 118, i32 83, i32 193, i32 233, i32 151, i32 175, i32 38, i32 204, ; 496..503
	i32 121, i32 145, i32 48, i32 255, i32 276, i32 64, i32 138, i32 115, ; 504..511
	i32 35, i32 138, i32 79, i32 161, i32 223, i32 211, i32 12, i32 181, ; 512..519
	i32 31, i32 271, i32 226, i32 274, i32 87, i32 208, i32 269, i32 111, ; 520..527
	i32 169, i32 197, i32 103, i32 244, i32 92, i32 120, i32 268, i32 132, ; 528..535
	i32 94, i32 149, i32 75, i32 6, i32 67, i32 107, i32 187, i32 278, ; 536..543
	i32 109, i32 49, i32 277, i32 215, i32 36, i32 117, i32 201, i32 22, ; 544..551
	i32 254, i32 118, i32 131, i32 180, i32 116, i32 243, i32 73, i32 201 ; 560..559
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 4; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 4

; Function attributes: "frame-pointer"="all" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 4
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 4
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"min_enum_size", i32 4}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}
