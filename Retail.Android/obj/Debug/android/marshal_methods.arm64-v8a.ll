; ModuleID = 'obj/Debug/android/marshal_methods.arm64-v8a.ll'
source_filename = "obj/Debug/android/marshal_methods.arm64-v8a.ll"
target datalayout = "e-m:e-i8:8:32-i16:16:32-i64:64-i128:128-n32:64-S128"
target triple = "aarch64-unknown-linux-android"


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
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 8
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [560 x i64] [
	i64 2646484529726201, ; 0: FFImageLoading.Forms.Platform => 0x966f6b24c42f9 => 21
	i64 24362543149721218, ; 1: Xamarin.AndroidX.DynamicAnimation => 0x568d9a9a43a682 => 186
	i64 30579257353033782, ; 2: Syncfusion.Buttons.XForms => 0x6ca3ac2c05d836 => 86
	i64 98382396393917666, ; 3: Microsoft.Extensions.Primitives.dll => 0x15d8644ad360ce2 => 53
	i64 120698629574877762, ; 4: Mono.Android => 0x1accec39cafe242 => 54
	i64 181099460066822533, ; 5: Microcharts.Droid.dll => 0x28364ffda4c4985 => 30
	i64 210515253464952879, ; 6: Xamarin.AndroidX.Collection.dll => 0x2ebe681f694702f => 175
	i64 229321459903363178, ; 7: Syncfusion.Cards.XForms => 0x32eb6a71ce9f86a => 88
	i64 232391251801502327, ; 8: Xamarin.AndroidX.SavedState.dll => 0x3399e9cbc897277 => 216
	i64 263803244706540312, ; 9: Rg.Plugins.Popup.dll => 0x3a937a743542b18 => 71
	i64 295915112840604065, ; 10: Xamarin.AndroidX.SlidingPaneLayout => 0x41b4d3a3088a9a1 => 217
	i64 316157742385208084, ; 11: Xamarin.AndroidX.Core.Core.Ktx.dll => 0x46337caa7dc1b14 => 180
	i64 347331204332682223, ; 12: ImageCircle.Forms.Plugin => 0x4d1f7e3dda87bef => 25
	i64 409821846381118697, ; 13: Xamarin.Forms.DebugRainbows => 0x5affacc46bdc4e9 => 233
	i64 464346026994987652, ; 14: System.Reactive.dll => 0x671b04057e67284 => 123
	i64 533980546060132701, ; 15: Microsoft.AppCenter.Analytics.dll => 0x769147a3ce2215d => 33
	i64 590536689428908136, ; 16: Xamarin.Android.Arch.Lifecycle.ViewModel.dll => 0x83201fd803ec868 => 139
	i64 627200827541438871, ; 17: OxyPlot.Xamarin.Forms.Platform.Android => 0x8b443d460704197 => 64
	i64 634308326490598313, ; 18: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x8cd840fee8b6ba9 => 198
	i64 687654259221141486, ; 19: Xamarin.GooglePlayServices.Base => 0x98b09e7c92917ee => 241
	i64 702024105029695270, ; 20: System.Drawing.Common => 0x9be17343c0e7726 => 10
	i64 720058930071658100, ; 21: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x9fe29c82844de74 => 191
	i64 761608270782653079, ; 22: Syncfusion.SfBusyIndicator.XForms.Android => 0xa91c6afe5ffe297 => 99
	i64 816102801403336439, ; 23: Xamarin.Android.Support.Collections => 0xb53612c89d8d6f7 => 143
	i64 822256607215579516, ; 24: Microsoft.AppCenter.Analytics.Android.Bindings.dll => 0xb693e071b4d717c => 32
	i64 846634227898301275, ; 25: Xamarin.Android.Arch.Lifecycle.LiveData.Core => 0xbbfd9583890bb5b => 136
	i64 870603111519317375, ; 26: SQLitePCLRaw.lib.e_sqlite3.android => 0xc1500ead2756d7f => 82
	i64 872800313462103108, ; 27: Xamarin.AndroidX.DrawerLayout => 0xc1ccf42c3c21c44 => 185
	i64 887546508555532406, ; 28: Microcharts.Forms => 0xc5132d8dc173876 => 31
	i64 940822596282819491, ; 29: System.Transactions => 0xd0e792aa81923a3 => 259
	i64 996343623809489702, ; 30: Xamarin.Forms.Platform => 0xdd3b93f3b63db26 => 237
	i64 1000557547492888992, ; 31: Mono.Security.dll => 0xde2b1c9cba651a0 => 278
	i64 1067864371574734079, ; 32: Retail.Android => 0xed1d0fcf801fcff => 0
	i64 1119473885309432845, ; 33: Shiny.Core.dll => 0xf892b914531f40d => 72
	i64 1120440138749646132, ; 34: Xamarin.Google.Android.Material.dll => 0xf8c9a5eae431534 => 239
	i64 1202444765235964449, ; 35: Syncfusion.Expander.XForms.dll => 0x10aff124a5eaea21 => 93
	i64 1301485588176585670, ; 36: SQLitePCLRaw.core => 0x120fce3f338e43c6 => 81
	i64 1310441165475816153, ; 37: Plugin.DeviceInfo => 0x122f9f4c073c22d9 => 66
	i64 1315114680217950157, ; 38: Xamarin.AndroidX.Arch.Core.Common.dll => 0x124039d5794ad7cd => 170
	i64 1342439039765371018, ; 39: Xamarin.Android.Support.Fragment => 0x12a14d31b1d4d88a => 152
	i64 1400031058434376889, ; 40: Plugin.Permissions.dll => 0x136de8d4787ec4b9 => 69
	i64 1416135423712704079, ; 41: Microcharts => 0x13a71faa343e364f => 29
	i64 1425944114962822056, ; 42: System.Runtime.Serialization.dll => 0x13c9f89e19eaf3a8 => 15
	i64 1435064484980070193, ; 43: Syncfusion.SfComboBox.XForms.dll => 0x13ea5f8bb9041731 => 104
	i64 1451832606041849089, ; 44: SignaturePad.Forms.dll => 0x1425f21024743d01 => 75
	i64 1476839205573959279, ; 45: System.Net.Primitives.dll => 0x147ec96ece9b1e6f => 265
	i64 1490981186906623721, ; 46: Xamarin.Android.Support.VersionedParcelable => 0x14b1077d6c5c0ee9 => 163
	i64 1518315023656898250, ; 47: SQLitePCLRaw.provider.e_sqlite3 => 0x151223783a354eca => 83
	i64 1586799124651907640, ; 48: Octane.Xamarin.Forms.VideoPlayer.Android.dll => 0x160571658ac71a38 => 59
	i64 1594001903634921555, ; 49: Octane.Xamarin.Forms.VideoPlayer.Android => 0x161f08493574b853 => 59
	i64 1624659445732251991, ; 50: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0x168bf32877da9957 => 168
	i64 1628611045998245443, ; 51: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0x1699fd1e1a00b643 => 201
	i64 1636321030536304333, ; 52: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0x16b5614ec39e16cd => 192
	i64 1731380447121279447, ; 53: Newtonsoft.Json => 0x18071957e9b889d7 => 58
	i64 1743969030606105336, ; 54: System.Memory.dll => 0x1833d297e88f2af8 => 268
	i64 1744702963312407042, ; 55: Xamarin.Android.Support.v7.AppCompat => 0x18366e19eeceb202 => 161
	i64 1785689246005058196, ; 56: Xamanimation => 0x18c80ae8835dda94 => 132
	i64 1795316252682057001, ; 57: Xamarin.AndroidX.AppCompat.dll => 0x18ea3e9eac997529 => 169
	i64 1836611346387731153, ; 58: Xamarin.AndroidX.SavedState => 0x197cf449ebe482d1 => 216
	i64 1865037103900624886, ; 59: Microsoft.Bcl.AsyncInterfaces => 0x19e1f15d56eb87f6 => 46
	i64 1875917498431009007, ; 60: Xamarin.AndroidX.Annotation.dll => 0x1a08990699eb70ef => 166
	i64 1938067011858688285, ; 61: Xamarin.Android.Support.v4.dll => 0x1ae565add0bd691d => 160
	i64 1981742497975770890, ; 62: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x1b80904d5c241f0a => 200
	i64 1984538867944326539, ; 63: FFImageLoading.Platform.dll => 0x1b8a7f95fac7058b => 22
	i64 1986553961460820075, ; 64: Xamarin.CommunityToolkit => 0x1b91a84d8004686b => 230
	i64 2040001226662520565, ; 65: System.Threading.Tasks.Extensions.dll => 0x1c4f8a4ea894a6f5 => 279
	i64 2064708342624596306, ; 66: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x1ca7514c5eecb152 => 248
	i64 2076847052340733488, ; 67: Syncfusion.Core.XForms => 0x1cd27163f7962630 => 90
	i64 2126915263223078201, ; 68: Syncfusion.GridCommon.Portable => 0x1d845229bbc49d39 => 94
	i64 2133195048986300728, ; 69: Newtonsoft.Json.dll => 0x1d9aa1984b735138 => 58
	i64 2136356949452311481, ; 70: Xamarin.AndroidX.MultiDex.dll => 0x1da5dd539d8acbb9 => 205
	i64 2165725771938924357, ; 71: Xamarin.AndroidX.Browser => 0x1e0e341d75540745 => 173
	i64 2197658138908603915, ; 72: Microsoft.AppCenter.Android.Bindings.dll => 0x1e7fa66f0364b60b => 34
	i64 2262844636196693701, ; 73: Xamarin.AndroidX.DrawerLayout.dll => 0x1f673d352266e6c5 => 185
	i64 2284400282711631002, ; 74: System.Web.Services => 0x1fb3d1f42fd4249a => 273
	i64 2287834202362508563, ; 75: System.Collections.Concurrent => 0x1fc00515e8ce7513 => 12
	i64 2304837677853103545, ; 76: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0x1ffc6da80d5ed5b9 => 213
	i64 2329709569556905518, ; 77: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x2054ca829b447e2e => 195
	i64 2335503487726329082, ; 78: System.Text.Encodings.Web => 0x2069600c4d9d1cfa => 126
	i64 2337758774805907496, ; 79: System.Runtime.CompilerServices.Unsafe => 0x207163383edbc828 => 124
	i64 2469392061734276978, ; 80: Syncfusion.Core.XForms.Android.dll => 0x22450aff2ad74f72 => 89
	i64 2470498323731680442, ; 81: Xamarin.AndroidX.CoordinatorLayout => 0x2248f922dc398cba => 179
	i64 2479423007379663237, ; 82: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x2268ae16b2cba985 => 224
	i64 2497223385847772520, ; 83: System.Runtime => 0x22a7eb7046413568 => 125
	i64 2541787113603107559, ; 84: Lottie.Android.dll => 0x23463de9b0fa8ae7 => 27
	i64 2547086958574651984, ; 85: Xamarin.AndroidX.Activity.dll => 0x2359121801df4a50 => 165
	i64 2592350477072141967, ; 86: System.Xml.dll => 0x23f9e10627330e8f => 129
	i64 2624866290265602282, ; 87: mscorlib.dll => 0x246d65fbde2db8ea => 56
	i64 2656907746661064104, ; 88: Microsoft.Extensions.DependencyInjection => 0x24df3b84c8b75da8 => 49
	i64 2694427813909235223, ; 89: Xamarin.AndroidX.Preference.dll => 0x256487d230fe0617 => 209
	i64 2749910993029543237, ; 90: Microsoft.AppCenter.Crashes => 0x2629a57a7f77b545 => 36
	i64 2783046991838674048, ; 91: System.Runtime.CompilerServices.Unsafe.dll => 0x269f5e7e6dc37c80 => 124
	i64 2787234703088983483, ; 92: Xamarin.AndroidX.Startup.StartupRuntime => 0x26ae3f31ef429dbb => 220
	i64 2801558180824670388, ; 93: Plugin.CurrentActivity.dll => 0x26e1225279a4e0b4 => 65
	i64 2863324215353042462, ; 94: FFImageLoading.Forms => 0x27bc92340ce4661e => 20
	i64 2949706848458024531, ; 95: Xamarin.Android.Support.SlidingPaneLayout => 0x28ef76c01de0a653 => 158
	i64 2960931600190307745, ; 96: Xamarin.Forms.Core => 0x2917579a49927da1 => 232
	i64 2977248461349026546, ; 97: Xamarin.Android.Support.DrawerLayout => 0x29514fb392c97af2 => 151
	i64 3015476561956492396, ; 98: TimeZoneConverter.dll => 0x29d91ff4d7eed46c => 131
	i64 3017704767998173186, ; 99: Xamarin.Google.Android.Material => 0x29e10a7f7d88a002 => 239
	i64 3022227708164871115, ; 100: Xamarin.Android.Support.Media.Compat.dll => 0x29f11c168f8293cb => 156
	i64 3122911337338800527, ; 101: Microcharts.dll => 0x2b56cf50bf1e898f => 29
	i64 3171992396844006720, ; 102: Square.OkIO => 0x2c052e476c207d40 => 84
	i64 3217286949467762653, ; 103: Syncfusion.SfChart.XForms.Android.dll => 0x2ca6196f4386afdd => 101
	i64 3260998928894807349, ; 104: Lottie.Forms.dll => 0x2d41653f91b44d35 => 28
	i64 3289520064315143713, ; 105: Xamarin.AndroidX.Lifecycle.Common => 0x2da6b911e3063621 => 194
	i64 3303437397778967116, ; 106: Xamarin.AndroidX.Annotation.Experimental => 0x2dd82acf985b2a4c => 167
	i64 3308638413622236918, ; 107: Shiny.Core => 0x2deaa51b762a56f6 => 72
	i64 3311221304742556517, ; 108: System.Numerics.Vectors.dll => 0x2df3d23ba9e2b365 => 122
	i64 3339480741387858005, ; 109: Xamarin.AndroidX.Work.Runtime => 0x2e58380a7cae7055 => 229
	i64 3344514922410554693, ; 110: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x2e6a1a9a18463545 => 251
	i64 3411255996856937470, ; 111: Xamarin.GooglePlayServices.Basement => 0x2f5737416a942bfe => 242
	i64 3493805808809882663, ; 112: Xamarin.AndroidX.Tracing.Tracing.dll => 0x307c7ddf444f3427 => 222
	i64 3522470458906976663, ; 113: Xamarin.AndroidX.SwipeRefreshLayout => 0x30e2543832f52197 => 221
	i64 3531994851595924923, ; 114: System.Numerics => 0x31042a9aade235bb => 121
	i64 3571415421602489686, ; 115: System.Runtime.dll => 0x319037675df7e556 => 125
	i64 3609787854626478660, ; 116: Plugin.CurrentActivity => 0x32188aeda587da44 => 65
	i64 3647754201059316852, ; 117: System.Xml.ReaderWriter => 0x329f6d1e86145474 => 274
	i64 3716579019761409177, ; 118: netstandard.dll => 0x3393f0ed5c8c5c99 => 57
	i64 3727469159507183293, ; 119: Xamarin.AndroidX.RecyclerView => 0x33baa1739ba646bd => 212
	i64 3730887114094830029, ; 120: Syncfusion.GridCommon.Portable.dll => 0x33c6c6102cb461cd => 94
	i64 3756217500168923014, ; 121: Syncfusion.SfBusyIndicator.XForms => 0x3420c3ea44aca786 => 100
	i64 3772598417116884899, ; 122: Xamarin.AndroidX.DynamicAnimation.dll => 0x345af645b473efa3 => 186
	i64 3783726507060260521, ; 123: Microsoft.AspNetCore.SignalR.Common.dll => 0x34827f360c8e6ea9 => 44
	i64 3869221888984012293, ; 124: Microsoft.Extensions.Logging.dll => 0x35b23cceda0ed605 => 51
	i64 3908336291285777176, ; 125: Syncfusion.SfNumericTextBox.XForms.Android.dll => 0x363d332650db3b18 => 108
	i64 3943415582112705276, ; 126: Syncfusion.Buttons.XForms.dll => 0x36b9d3942d916afc => 86
	i64 3955305636023511547, ; 127: Microsoft.AppCenter.Crashes.Android.Bindings.dll => 0x36e41185154829fb => 35
	i64 3966267475168208030, ; 128: System.Memory => 0x370b03412596249e => 268
	i64 4201423742386704971, ; 129: Xamarin.AndroidX.Core.Core.Ktx => 0x3a4e74a233da124b => 180
	i64 4247996603072512073, ; 130: Xamarin.GooglePlayServices.Tasks => 0x3af3ea6755340049 => 244
	i64 4248804899347366872, ; 131: Xamarin.AndroidX.Room.Common.dll => 0x3af6c98b798a03d8 => 214
	i64 4252163538099460320, ; 132: Xamarin.Android.Support.ViewPager.dll => 0x3b02b8357f4958e0 => 164
	i64 4255796613242758200, ; 133: zxing.portable => 0x3b0fa078b8a52438 => 257
	i64 4292233171264798357, ; 134: ZXing.Net.Mobile.Core.dll => 0x3b911353fa62fe95 => 254
	i64 4334352720302891708, ; 135: Plugin.Iconize.dll => 0x3c26b6d5b0e6e2bc => 67
	i64 4337444564132831293, ; 136: SQLitePCLRaw.batteries_v2.dll => 0x3c31b2d9ae16203d => 80
	i64 4349341163892612332, ; 137: Xamarin.Android.Support.DocumentFile => 0x3c5bf6bea8cd9cec => 150
	i64 4416987920449902723, ; 138: Xamarin.Android.Support.AsyncLayoutInflater.dll => 0x3d4c4b1c879b9883 => 142
	i64 4424758928462204584, ; 139: Xamanimation.dll => 0x3d67e6cd53ba4ea8 => 132
	i64 4525561845656915374, ; 140: System.ServiceModel.Internals => 0x3ece06856b710dae => 260
	i64 4636684751163556186, ; 141: Xamarin.AndroidX.VersionedParcelable.dll => 0x4058d0370893015a => 226
	i64 4747525699245845799, ; 142: Xamarin.Forms.DebugRainbows.dll => 0x41e2997851962d27 => 233
	i64 4759461199762736555, ; 143: Xamarin.AndroidX.Lifecycle.Process.dll => 0x420d00be961cc5ab => 197
	i64 4782108999019072045, ; 144: Xamarin.AndroidX.AsyncLayoutInflater.dll => 0x425d76cc43bb0a2d => 172
	i64 4794310189461587505, ; 145: Xamarin.AndroidX.Activity => 0x4288cfb749e4c631 => 165
	i64 4795410492532947900, ; 146: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0x428cb86f8f9b7bbc => 221
	i64 4841234827713643511, ; 147: Xamarin.Android.Support.CoordinaterLayout => 0x432f856d041f03f7 => 145
	i64 4906396365959678531, ; 148: Syncfusion.SfPicker.XForms => 0x4417057fe84b4a43 => 112
	i64 4963205065368577818, ; 149: Xamarin.Android.Support.LocalBroadcastManager.dll => 0x44e0d8b5f4b6a71a => 155
	i64 4993330139331155028, ; 150: Syncfusion.SfRadialMenu.Android => 0x454bdf4e5115d454 => 113
	i64 5081566143765835342, ; 151: System.Resources.ResourceManager.dll => 0x4685597c05d06e4e => 2
	i64 5099468265966638712, ; 152: System.Resources.ResourceManager => 0x46c4f35ea8519678 => 2
	i64 5142919913060024034, ; 153: Xamarin.Forms.Platform.Android.dll => 0x475f52699e39bee2 => 236
	i64 5155409377101935888, ; 154: Syncfusion.SfRadialMenu.XForms => 0x478bb18391e0f510 => 115
	i64 5178572682164047940, ; 155: Xamarin.Android.Support.Print.dll => 0x47ddfc6acbee1044 => 157
	i64 5202753749449073649, ; 156: Plugin.Media => 0x4833e4f841be63f1 => 68
	i64 5203618020066742981, ; 157: Xamarin.Essentials => 0x4836f704f0e652c5 => 231
	i64 5205316157927637098, ; 158: Xamarin.AndroidX.LocalBroadcastManager => 0x483cff7778e0c06a => 203
	i64 5233983725610684227, ; 159: FastAndroidCamera => 0x48a2d877b5334f43 => 18
	i64 5256995213548036366, ; 160: Xamarin.Forms.Maps.Android.dll => 0x48f4994b4175a10e => 234
	i64 5286637047618374099, ; 161: OxyPlot => 0x495de8628fb689d3 => 61
	i64 5288341611614403055, ; 162: Xamarin.Android.Support.Interpolator.dll => 0x4963f6ad4b3179ef => 153
	i64 5348796042099802469, ; 163: Xamarin.AndroidX.Media => 0x4a3abda9415fc165 => 204
	i64 5376510917114486089, ; 164: Xamarin.AndroidX.VectorDrawable.Animated => 0x4a9d3431719e5d49 => 224
	i64 5408338804355907810, ; 165: Xamarin.AndroidX.Transition => 0x4b0e477cea9840e2 => 223
	i64 5439315836349573567, ; 166: Xamarin.Android.Support.Animated.Vector.Drawable.dll => 0x4b7c54ef36c5e9bf => 140
	i64 5446034149219586269, ; 167: System.Diagnostics.Debug => 0x4b94333452e150dd => 5
	i64 5451019430259338467, ; 168: Xamarin.AndroidX.ConstraintLayout.dll => 0x4ba5e94a845c2ce3 => 178
	i64 5507995362134886206, ; 169: System.Core.dll => 0x4c705499688c873e => 117
	i64 5514426807633697079, ; 170: Xamarin.AndroidX.Sqlite => 0x4c872df700e5d937 => 218
	i64 5540976586714296940, ; 171: Syncfusion.SfNumericTextBox.Android.dll => 0x4ce580d927dcb26c => 107
	i64 5619971803549996551, ; 172: Microsoft.AppCenter => 0x4dfe2694564f1607 => 37
	i64 5692067934154308417, ; 173: Xamarin.AndroidX.ViewPager2.dll => 0x4efe49a0d4a8bb41 => 228
	i64 5757522595884336624, ; 174: Xamarin.AndroidX.Concurrent.Futures.dll => 0x4fe6d44bd9f885f0 => 176
	i64 5759039185982771185, ; 175: Xamarin.AndroidX.Lifecycle.Service => 0x4fec37a0800ecff1 => 199
	i64 5767696078500135884, ; 176: Xamarin.Android.Support.Annotations.dll => 0x500af9065b6a03cc => 141
	i64 5767749323661124970, ; 177: ZXing.Net.Mobile.Core => 0x500b29737652256a => 254
	i64 5775268978821181986, ; 178: Syncfusion.SfBusyIndicator.Android => 0x5025e0899cf83222 => 98
	i64 5814345312393086621, ; 179: Xamarin.AndroidX.Preference => 0x50b0b44182a5c69d => 209
	i64 5819465594466874502, ; 180: SignaturePad.Forms => 0x50c2e52014ce3486 => 75
	i64 5848635860608528381, ; 181: Syncfusion.SfPicker.Android => 0x512a8753ec2eaffd => 110
	i64 5880754220653401751, ; 182: Retail => 0x519ca2ce5d6cd697 => 70
	i64 5896680224035167651, ; 183: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x51d5376bfbafdda3 => 196
	i64 6014447449592687183, ; 184: Microsoft.AspNetCore.Http.Connections.Common.dll => 0x53779c16e939ea4f => 40
	i64 6034224070161570862, ; 185: Microsoft.AspNetCore.SignalR.Client.dll => 0x53bdded235179c2e => 43
	i64 6044705416426755068, ; 186: Xamarin.Android.Support.SwipeRefreshLayout.dll => 0x53e31b8ccdff13fc => 159
	i64 6046773784300663497, ; 187: Syncfusion.SfRadialMenu.Android.dll => 0x53ea74b83a62b2c9 => 113
	i64 6085203216496545422, ; 188: Xamarin.Forms.Platform.dll => 0x5472fc15a9574e8e => 237
	i64 6086316965293125504, ; 189: FormsViewGroup.dll => 0x5476f10882baef80 => 23
	i64 6183170893902868313, ; 190: SQLitePCLRaw.batteries_v2 => 0x55cf092b0c9d6f59 => 80
	i64 6222399776351216807, ; 191: System.Text.Json.dll => 0x565a67a0ffe264a7 => 127
	i64 6311200438583329442, ; 192: Xamarin.Android.Support.LocalBroadcastManager => 0x5795e35c580c82a2 => 155
	i64 6319713645133255417, ; 193: Xamarin.AndroidX.Lifecycle.Runtime => 0x57b42213b45b52f9 => 198
	i64 6401687960814735282, ; 194: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0x58d75d486341cfb2 => 195
	i64 6405879832841858445, ; 195: Xamarin.Android.Support.Vector.Drawable.dll => 0x58e641c4a660ad8d => 162
	i64 6504860066809920875, ; 196: Xamarin.AndroidX.Browser.dll => 0x5a45e7c43bd43d6b => 173
	i64 6548213210057960872, ; 197: Xamarin.AndroidX.CustomView.dll => 0x5adfed387b066da8 => 183
	i64 6560151584539558821, ; 198: Microsoft.Extensions.Options => 0x5b0a571be53243a5 => 52
	i64 6588599331800941662, ; 199: Xamarin.Android.Support.v4 => 0x5b6f682f335f045e => 160
	i64 6591024623626361694, ; 200: System.Web.Services.dll => 0x5b7805f9751a1b5e => 273
	i64 6659513131007730089, ; 201: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0x5c6b57e8b6c3e1a9 => 191
	i64 6671798237668743565, ; 202: SkiaSharp => 0x5c96fd260152998d => 76
	i64 6711679492026985136, ; 203: Syncfusion.SfNumericTextBox.XForms.dll => 0x5d24acf0208aeab0 => 109
	i64 6783125919820072783, ; 204: Microsoft.AspNetCore.Connections.Abstractions => 0x5e228115e59ec74f => 38
	i64 6876862101832370452, ; 205: System.Xml.Linq => 0x5f6f85a57d108914 => 130
	i64 6878024704864574184, ; 206: Syncfusion.Cards.XForms.dll => 0x5f73a70719d05ae8 => 88
	i64 6894844156784520562, ; 207: System.Numerics.Vectors => 0x5faf683aead1ad72 => 122
	i64 7017588408768804231, ; 208: Microsoft.AspNetCore.SignalR.Protocols.Json => 0x61637b7a1c903587 => 45
	i64 7026608356547306326, ; 209: Syncfusion.Core.XForms.dll => 0x618387125bcb2356 => 90
	i64 7036436454368433159, ; 210: Xamarin.AndroidX.Legacy.Support.V4.dll => 0x61a671acb33d5407 => 193
	i64 7103753931438454322, ; 211: Xamarin.AndroidX.Interpolator.dll => 0x62959a90372c7632 => 190
	i64 7111211609209905225, ; 212: Xamarin.Plugin.Calendar => 0x62b0194821972049 => 252
	i64 7141281584637745974, ; 213: Xamarin.GooglePlayServices.Maps.dll => 0x631aedc3dd5f1b36 => 243
	i64 7141577505875122296, ; 214: System.Runtime.InteropServices.WindowsRuntime.dll => 0x631bfae7659b5878 => 13
	i64 7194160955514091247, ; 215: Xamarin.Android.Support.CursorAdapter.dll => 0x63d6cb45d266f6ef => 148
	i64 7270811800166795866, ; 216: System.Linq => 0x64e71ccf51a90a5a => 263
	i64 7270951509819434961, ; 217: Syncfusion.SfAutoComplete.XForms => 0x64e79be001e0a7d1 => 97
	i64 7291284685109936435, ; 218: Microsoft.AppCenter.Analytics => 0x652fd8ca4c394133 => 33
	i64 7338192458477945005, ; 219: System.Reflection => 0x65d67f295d0740ad => 4
	i64 7363614467969488359, ; 220: Xamarin.AndroidX.Sqlite.Framework.dll => 0x6630d058323f95e7 => 219
	i64 7364333345899356075, ; 221: DLToolkit.Forms.Controls.FlowListView => 0x66335e2901e51fab => 17
	i64 7488575175965059935, ; 222: System.Xml.Linq.dll => 0x67ecc3724534ab5f => 130
	i64 7489048572193775167, ; 223: System.ObjectModel => 0x67ee71ff6b419e3f => 261
	i64 7566872048948821773, ; 224: Syncfusion.SfDataGrid.XForms => 0x6902ee099a6d3f0d => 106
	i64 7602111570124318452, ; 225: System.Reactive => 0x698020320025a6f4 => 123
	i64 7635363394907363464, ; 226: Xamarin.Forms.Core.dll => 0x69f6428dc4795888 => 232
	i64 7637365915383206639, ; 227: Xamarin.Essentials.dll => 0x69fd5fd5e61792ef => 231
	i64 7654504624184590948, ; 228: System.Net.Http => 0x6a3a4366801b8264 => 14
	i64 7735176074855944702, ; 229: Microsoft.CSharp => 0x6b58dda848e391fe => 47
	i64 7735352534559001595, ; 230: Xamarin.Kotlin.StdLib.dll => 0x6b597e2582ce8bfb => 247
	i64 7767211987177345230, ; 231: Syncfusion.SfPicker.Android.dll => 0x6bcaae265edc90ce => 110
	i64 7820441508502274321, ; 232: System.Data => 0x6c87ca1e14ff8111 => 11
	i64 7821246742157274664, ; 233: Xamarin.Android.Support.AsyncLayoutInflater => 0x6c8aa67926f72e28 => 142
	i64 7836164640616011524, ; 234: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x6cbfa6390d64d704 => 168
	i64 7842383726582361265, ; 235: Xamarin.AndroidX.Sqlite.dll => 0x6cd5be72d73eecb1 => 218
	i64 7875371864198757046, ; 236: AndHUD.dll => 0x6d4af0fc27ac3ab6 => 16
	i64 7879037620440914030, ; 237: Xamarin.Android.Support.v7.AppCompat.dll => 0x6d57f6f88a51d86e => 161
	i64 7927939710195668715, ; 238: SkiaSharp.Views.Android.dll => 0x6e05b32992ed16eb => 77
	i64 7956423435673659877, ; 239: Syncfusion.SfDataGrid.XForms.Android => 0x6e6ae4f5b5ebe9e5 => 105
	i64 8044118961405839122, ; 240: System.ComponentModel.Composition => 0x6fa2739369944712 => 272
	i64 8064050204834738623, ; 241: System.Collections.dll => 0x6fe942efa61731bf => 3
	i64 8083054321397151520, ; 242: Syncfusion.SfDataGrid.XForms.Android.dll => 0x702cc71457103720 => 105
	i64 8083354569033831015, ; 243: Xamarin.AndroidX.Lifecycle.Common.dll => 0x702dd82730cad267 => 194
	i64 8087206902342787202, ; 244: System.Diagnostics.DiagnosticSource => 0x703b87d46f3aa082 => 118
	i64 8101777744205214367, ; 245: Xamarin.Android.Support.Annotations => 0x706f4beeec84729f => 141
	i64 8103644804370223335, ; 246: System.Data.DataSetExtensions.dll => 0x7075ee03be6d50e7 => 269
	i64 8113615946733131500, ; 247: System.Reflection.Extensions => 0x70995ab73cf916ec => 8
	i64 8167236081217502503, ; 248: Java.Interop.dll => 0x7157d9f1a9b8fd27 => 26
	i64 8185542183669246576, ; 249: System.Collections => 0x7198e33f4794aa70 => 3
	i64 8187102936927221770, ; 250: SkiaSharp.Views.Forms => 0x719e6ebe771ab80a => 78
	i64 8187640529827139739, ; 251: Xamarin.KotlinX.Coroutines.Android => 0x71a057ae90f0109b => 250
	i64 8196541262927413903, ; 252: Xamarin.Android.Support.Interpolator => 0x71bff6d9fb9ec28f => 153
	i64 8243855692487634729, ; 253: Microsoft.AspNetCore.SignalR.Protocols.Json.dll => 0x72680f13124eaf29 => 45
	i64 8290740647658429042, ; 254: System.Runtime.Extensions => 0x730ea0b15c929a72 => 7
	i64 8377847505162989171, ; 255: OxyPlot.Xamarin.Forms => 0x744417eb0fa1ee73 => 63
	i64 8385935383968044654, ; 256: Xamarin.Android.Arch.Lifecycle.Runtime.dll => 0x7460d3cd16cb566e => 138
	i64 8398329775253868912, ; 257: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x748cdc6f3097d170 => 177
	i64 8400357532724379117, ; 258: Xamarin.AndroidX.Navigation.UI.dll => 0x749410ab44503ded => 208
	i64 8426919725312979251, ; 259: Xamarin.AndroidX.Lifecycle.Process => 0x74f26ed7aa033133 => 197
	i64 8517710342570482946, ; 260: Syncfusion.Buttons.XForms.Android => 0x7634fc6d84959d02 => 85
	i64 8598790081731763592, ; 261: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x77550a055fc61d88 => 188
	i64 8601935802264776013, ; 262: Xamarin.AndroidX.Transition.dll => 0x7760370982b4ed4d => 223
	i64 8609060182490045521, ; 263: Square.OkIO.dll => 0x7779869f8b475c51 => 84
	i64 8626175481042262068, ; 264: Java.Interop => 0x77b654e585b55834 => 26
	i64 8638972117149407195, ; 265: Microsoft.CSharp.dll => 0x77e3cb5e8b31d7db => 47
	i64 8639588376636138208, ; 266: Xamarin.AndroidX.Navigation.Runtime => 0x77e5fbdaa2fda2e0 => 207
	i64 8684531736582871431, ; 267: System.IO.Compression.FileSystem => 0x7885a79a0fa0d987 => 271
	i64 8725526185868997716, ; 268: System.Diagnostics.DiagnosticSource.dll => 0x79174bd613173454 => 118
	i64 8808820144457481518, ; 269: Xamarin.Android.Support.Loader.dll => 0x7a3f374010b17d2e => 154
	i64 8853378295825400934, ; 270: Xamarin.Kotlin.StdLib.Common.dll => 0x7add84a720d38466 => 246
	i64 8885868990357824044, ; 271: Octane.Xamarin.Forms.VideoPlayer => 0x7b50f2c472efe22c => 60
	i64 8917102979740339192, ; 272: Xamarin.Android.Support.DocumentFile.dll => 0x7bbfe9ea4d000bf8 => 150
	i64 8951477988056063522, ; 273: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0x7c3a09cd9ccf5e22 => 211
	i64 9034105039140296321, ; 274: Syncfusion.SfChart.XForms => 0x7d5f96ab19861681 => 102
	i64 9083778504339266700, ; 275: OxyPlot.Xamarin.Android.dll => 0x7e10106bf9789c8c => 62
	i64 9114191852432800567, ; 276: FFImageLoading.dll => 0x7e7c1d3363043b37 => 19
	i64 9238306115887712111, ; 277: FFImageLoading.Forms.dll => 0x80350e773bce476f => 20
	i64 9238909584418939062, ; 278: Xamarin.AndroidX.Sqlite.Framework => 0x80373351333e20b6 => 219
	i64 9293890220217345133, ; 279: Syncfusion.SfAutoComplete.XForms.Android.dll => 0x80fa87ea0588246d => 96
	i64 9312692141327339315, ; 280: Xamarin.AndroidX.ViewPager2 => 0x813d54296a634f33 => 228
	i64 9324707631942237306, ; 281: Xamarin.AndroidX.AppCompat => 0x8168042fd44a7c7a => 169
	i64 9418564226550032778, ; 282: Syncfusion.SfNumericTextBox.XForms.Android => 0x82b5764329b7698a => 108
	i64 9584643793929893533, ; 283: System.IO.dll => 0x85037ebfbbd7f69d => 264
	i64 9653670174411988578, ; 284: Syncfusion.SfPicker.XForms.Android => 0x85f8b9e0549c1e62 => 111
	i64 9659729154652888475, ; 285: System.Text.RegularExpressions => 0x860e407c9991dd9b => 266
	i64 9662334977499516867, ; 286: System.Numerics.dll => 0x8617827802b0cfc3 => 121
	i64 9678050649315576968, ; 287: Xamarin.AndroidX.CoordinatorLayout.dll => 0x864f57c9feb18c88 => 179
	i64 9711637524876806384, ; 288: Xamarin.AndroidX.Media.dll => 0x86c6aadfd9a2c8f0 => 204
	i64 9754517241374622406, ; 289: Syncfusion.SfAutoComplete.XForms.Android => 0x875f01bfd78ec2c6 => 96
	i64 9780723996889434231, ; 290: AndHUD => 0x87bc1ca798bbc877 => 16
	i64 9808709177481450983, ; 291: Mono.Android.dll => 0x881f890734e555e7 => 54
	i64 9825649861376906464, ; 292: Xamarin.AndroidX.Concurrent.Futures => 0x885bb87d8abc94e0 => 176
	i64 9834056768316610435, ; 293: System.Transactions.dll => 0x8879968718899783 => 259
	i64 9866412715007501892, ; 294: Xamarin.Android.Arch.Lifecycle.Common.dll => 0x88ec8a16fd6b6644 => 135
	i64 9875200773399460291, ; 295: Xamarin.GooglePlayServices.Base.dll => 0x890bc2c8482339c3 => 241
	i64 9907349773706910547, ; 296: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x897dfa20b758db53 => 188
	i64 9998632235833408227, ; 297: Mono.Security => 0x8ac2470b209ebae3 => 278
	i64 10038780035334861115, ; 298: System.Net.Http.dll => 0x8b50e941206af13b => 14
	i64 10226222362177979215, ; 299: Xamarin.Kotlin.StdLib.Jdk7 => 0x8dead70ebbc6434f => 248
	i64 10229024438826829339, ; 300: Xamarin.AndroidX.CustomView => 0x8df4cb880b10061b => 183
	i64 10303855825347935641, ; 301: Xamarin.Android.Support.Loader => 0x8efea647eeb3fd99 => 154
	i64 10321854143672141184, ; 302: Xamarin.Jetbrains.Annotations.dll => 0x8f3e97a7f8f8c580 => 245
	i64 10360651442923773544, ; 303: System.Text.Encoding => 0x8fc86d98211c1e68 => 9
	i64 10363495123250631811, ; 304: Xamarin.Android.Support.Collections.dll => 0x8fd287e80cd8d483 => 143
	i64 10376576884623852283, ; 305: Xamarin.AndroidX.Tracing.Tracing => 0x900101b2f888c2fb => 222
	i64 10406448008575299332, ; 306: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x906b2153fcb3af04 => 251
	i64 10421511051509811956, ; 307: Xamarin.AndroidX.Room.Runtime.dll => 0x90a0a515f80ccaf4 => 215
	i64 10430153318873392755, ; 308: Xamarin.AndroidX.Core => 0x90bf592ea44f6673 => 181
	i64 10447083246144586668, ; 309: Microsoft.Bcl.AsyncInterfaces.dll => 0x90fb7edc816203ac => 46
	i64 10566960649245365243, ; 310: System.Globalization.dll => 0x92a562b96dcd13fb => 267
	i64 10576489213102601029, ; 311: Retail.Android.dll => 0x92c73ce715bb3345 => 0
	i64 10635644668885628703, ; 312: Xamarin.Android.Support.DrawerLayout.dll => 0x93996679ee34771f => 151
	i64 10679925812255520825, ; 313: Xamarin.AndroidX.Work.Runtime.dll => 0x9436b7f10b03f839 => 229
	i64 10714184849103829812, ; 314: System.Runtime.Extensions.dll => 0x94b06e5aa4b4bb34 => 7
	i64 10775409704848971057, ; 315: Xamarin.Forms.Maps => 0x9589f20936d1d531 => 235
	i64 10785150219063592792, ; 316: System.Net.Primitives => 0x95ac8cfb68830758 => 265
	i64 10847732767863316357, ; 317: Xamarin.AndroidX.Arch.Core.Common => 0x968ae37a86db9f85 => 170
	i64 10849603794298325860, ; 318: Xamarin.AndroidX.Room.Common => 0x9691892ad0e1cb64 => 214
	i64 10850923258212604222, ; 319: Xamarin.Android.Arch.Lifecycle.Runtime => 0x9696393672c9593e => 138
	i64 10878511855281532577, ; 320: Syncfusion.Cards.XForms.Android.dll => 0x96f83ce542ee6ea1 => 87
	i64 10964653383833615866, ; 321: System.Diagnostics.Tracing => 0x982a4628ccaffdfa => 275
	i64 11002576679268595294, ; 322: Microsoft.Extensions.Logging.Abstractions => 0x98b1013215cd365e => 50
	i64 11023048688141570732, ; 323: System.Core => 0x98f9bc61168392ac => 117
	i64 11034181033432536911, ; 324: Syncfusion.SfNumericTextBox.XForms => 0x992149303519574f => 109
	i64 11037814507248023548, ; 325: System.Xml => 0x992e31d0412bf7fc => 129
	i64 11050168729868392624, ; 326: Microsoft.AspNetCore.Http.Features => 0x995a15e9dbef58b0 => 41
	i64 11122995063473561350, ; 327: Xamarin.CommunityToolkit.dll => 0x9a5cd113fcc3df06 => 230
	i64 11162124722117608902, ; 328: Xamarin.AndroidX.ViewPager => 0x9ae7d54b986d05c6 => 227
	i64 11177418026010201814, ; 329: XF.Material => 0x9b1e2a796262ced6 => 253
	i64 11226290749488709958, ; 330: Microsoft.Extensions.Options.dll => 0x9bcbcbf50c874146 => 52
	i64 11340910727871153756, ; 331: Xamarin.AndroidX.CursorAdapter => 0x9d630238642d465c => 182
	i64 11376461258732682436, ; 332: Xamarin.Android.Support.Compat => 0x9de14f3d5fc13cc4 => 144
	i64 11392833485892708388, ; 333: Xamarin.AndroidX.Print.dll => 0x9e1b79b18fcf6824 => 210
	i64 11395515702206878257, ; 334: XF.Material.dll => 0x9e250127b66d5231 => 253
	i64 11432101114902388181, ; 335: System.AppContext => 0x9ea6fb64e61a9dd5 => 277
	i64 11444370155346000636, ; 336: Xamarin.Forms.Maps.Android => 0x9ed292057b7afafc => 234
	i64 11446671985764974897, ; 337: Mono.Android.Export => 0x9edabf8623efc131 => 55
	i64 11472067811128429120, ; 338: Microsoft.AppCenter.Crashes.Android.Bindings => 0x9f34f8e48180ca40 => 35
	i64 11481714388108425262, ; 339: Syncfusion.SfComboBox.XForms => 0x9f573e673bb1b82e => 104
	i64 11485890710487134646, ; 340: System.Runtime.InteropServices => 0x9f6614bf0f8b71b6 => 276
	i64 11513602507638267977, ; 341: System.IO.Pipelines.dll => 0x9fc8887aa0d36049 => 120
	i64 11529969570048099689, ; 342: Xamarin.AndroidX.ViewPager.dll => 0xa002ae3c4dc7c569 => 227
	i64 11530571088791430846, ; 343: Microsoft.Extensions.Logging => 0xa004d1504ccd66be => 51
	i64 11578238080964724296, ; 344: Xamarin.AndroidX.Legacy.Support.V4 => 0xa0ae2a30c4cd8648 => 193
	i64 11580057168383206117, ; 345: Xamarin.AndroidX.Annotation => 0xa0b4a0a4103262e5 => 166
	i64 11591352189662810718, ; 346: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0xa0dcc167234c525e => 220
	i64 11597940890313164233, ; 347: netstandard => 0xa0f429ca8d1805c9 => 57
	i64 11660723344418086924, ; 348: Plugin.Iconize => 0xa1d33619c025200c => 67
	i64 11666126733838079721, ; 349: Xamarin.Plugin.Calendar.dll => 0xa1e66874631b56e9 => 252
	i64 11672361001936329215, ; 350: Xamarin.AndroidX.Interpolator => 0xa1fc8e7d0a8999ff => 190
	i64 11683710219442713716, ; 351: ZXingNetMobile => 0xa224e08aa87bf474 => 258
	i64 11739066727115742305, ; 352: SQLite-net.dll => 0xa2e98afdf8575c61 => 79
	i64 11743665907891708234, ; 353: System.Threading.Tasks => 0xa2f9e1ec30c0214a => 262
	i64 11758626982801240232, ; 354: Syncfusion.SfBusyIndicator.XForms.Android.dll => 0xa32f08f0e430f0a8 => 99
	i64 11806260347154423189, ; 355: SQLite-net => 0xa3d8433bc5eb5d95 => 79
	i64 11834399401546345650, ; 356: Xamarin.Android.Support.SlidingPaneLayout.dll => 0xa43c3b8deb43ecb2 => 158
	i64 11854093697108963210, ; 357: Microsoft.AppCenter.Crashes.dll => 0xa48233696e606f8a => 36
	i64 11865714326292153359, ; 358: Xamarin.Android.Arch.Lifecycle.LiveData => 0xa4ab7c5000e8440f => 137
	i64 11869220915266023700, ; 359: Syncfusion.SfAutoComplete.XForms.dll => 0xa4b7f1895f117114 => 97
	i64 12006736334756399793, ; 360: SignaturePad => 0xa6a07f1500ee4ab1 => 74
	i64 12036099219279441448, ; 361: ZXing.Net.Mobile.Forms => 0xa708d0784e81ee28 => 256
	i64 12102847907131387746, ; 362: System.Buffers => 0xa7f5f40c43256f62 => 116
	i64 12123043025855404482, ; 363: System.Reflection.Extensions.dll => 0xa83db366c0e359c2 => 8
	i64 12137774235383566651, ; 364: Xamarin.AndroidX.VectorDrawable => 0xa872095bbfed113b => 225
	i64 12145679461940342714, ; 365: System.Text.Json => 0xa88e1f1ebcb62fba => 127
	i64 12250081775278992142, ; 366: Microsoft.AppCenter.Analytics.Android.Bindings => 0xaa0108788cfdab0e => 32
	i64 12263794765274154115, ; 367: Microsoft.AppCenter.dll => 0xaa31c05cd6760483 => 37
	i64 12271526709721397701, ; 368: Syncfusion.SfPicker.XForms.dll => 0xaa4d388670a979c5 => 112
	i64 12279246230491828964, ; 369: SQLitePCLRaw.provider.e_sqlite3.dll => 0xaa68a5636e0512e4 => 83
	i64 12313367145828839434, ; 370: System.IO.Pipelines => 0xaae1de2e1c17f00a => 120
	i64 12388767885335911387, ; 371: Xamarin.Android.Arch.Lifecycle.LiveData.dll => 0xabedbec0d236dbdb => 137
	i64 12414299427252656003, ; 372: Xamarin.Android.Support.Compat.dll => 0xac48738e28bad783 => 144
	i64 12450197211230333945, ; 373: SignaturePad.dll => 0xacc7fc664ef16bf9 => 74
	i64 12451044538927396471, ; 374: Xamarin.AndroidX.Fragment.dll => 0xaccaff0a2955b677 => 189
	i64 12466513435562512481, ; 375: Xamarin.AndroidX.Loader.dll => 0xad01f3eb52569061 => 202
	i64 12487638416075308985, ; 376: Xamarin.AndroidX.DocumentFile.dll => 0xad4d00fa21b0bfb9 => 184
	i64 12488608402635344228, ; 377: Lottie.Android => 0xad50732cba09c964 => 27
	i64 12538491095302438457, ; 378: Xamarin.AndroidX.CardView.dll => 0xae01ab382ae67e39 => 174
	i64 12550732019250633519, ; 379: System.IO.Compression => 0xae2d28465e8e1b2f => 270
	i64 12629983860853673214, ; 380: ZXing.Net.Mobile.Forms.Android.dll => 0xaf46b767a9198cfe => 255
	i64 12700543734426720211, ; 381: Xamarin.AndroidX.Collection => 0xb041653c70d157d3 => 175
	i64 12708238894395270091, ; 382: System.IO => 0xb05cbbf17d3ba3cb => 264
	i64 12828192437253469131, ; 383: Xamarin.Kotlin.StdLib.Jdk8.dll => 0xb206e50e14d873cb => 249
	i64 12832250852553794196, ; 384: Syncfusion.SfBusyIndicator.XForms.dll => 0xb2155029872c2294 => 100
	i64 12843321153144804894, ; 385: Microsoft.Extensions.Primitives => 0xb23ca48abd74d61e => 53
	i64 12843770487262409629, ; 386: System.AppContext.dll => 0xb23e3d357debf39d => 277
	i64 12952608645614506925, ; 387: Xamarin.Android.Support.Core.Utils => 0xb3c0e8eff48193ad => 147
	i64 12963446364377008305, ; 388: System.Drawing.Common.dll => 0xb3e769c8fd8548b1 => 10
	i64 13011563728930061243, ; 389: OxyPlot.dll => 0xb4925c45f33bbbbb => 61
	i64 13058651076603825364, ; 390: Syncfusion.Data.Portable.dll => 0xb539a5f76abe4cd4 => 91
	i64 13129914918964716986, ; 391: Xamarin.AndroidX.Emoji2.dll => 0xb636d40db3fe65ba => 187
	i64 13295219713260136977, ; 392: Microsoft.AspNetCore.Http.Connections.Client => 0xb8821be35ba42a11 => 39
	i64 13326567658044861122, ; 393: Retail.Infrastructure.dll => 0xb8f17aad84f92ac2 => 1
	i64 13341214209551883571, ; 394: Shiny.Notifications => 0xb92583a388bbdd33 => 73
	i64 13358059602087096138, ; 395: Xamarin.Android.Support.Fragment.dll => 0xb9615c6f1ee5af4a => 152
	i64 13370190879438847839, ; 396: Syncfusion.SfRadialMenu.XForms.Android.dll => 0xb98c75c43c1ebf5f => 114
	i64 13370592475155966277, ; 397: System.Runtime.Serialization => 0xb98de304062ea945 => 15
	i64 13391361154981494717, ; 398: Syncfusion.Buttons.XForms.Android.dll => 0xb9d7ac051da2ffbd => 85
	i64 13401370062847626945, ; 399: Xamarin.AndroidX.VectorDrawable.dll => 0xb9fb3b1193964ec1 => 225
	i64 13403416310143541304, ; 400: Microcharts.Droid => 0xba02801ea6c86038 => 30
	i64 13404347523447273790, ; 401: Xamarin.AndroidX.ConstraintLayout.Core => 0xba05cf0da4f6393e => 177
	i64 13407771833520603066, ; 402: Syncfusion.SfRadialMenu.XForms.Android => 0xba11f971f67bbfba => 114
	i64 13428779960367410341, ; 403: Microsoft.AspNetCore.SignalR.Client.Core.dll => 0xba5c9c39a8956ca5 => 42
	i64 13454009404024712428, ; 404: Xamarin.Google.Guava.ListenableFuture => 0xbab63e4543a86cec => 240
	i64 13465488254036897740, ; 405: Xamarin.Kotlin.StdLib => 0xbadf06394d106fcc => 247
	i64 13491513212026656886, ; 406: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0xbb3b7bc905569876 => 171
	i64 13492263892638604996, ; 407: SkiaSharp.Views.Forms.dll => 0xbb3e2686788d9ec4 => 78
	i64 13572454107664307259, ; 408: Xamarin.AndroidX.RecyclerView.dll => 0xbc5b0b19d99f543b => 212
	i64 13622732128915678507, ; 409: Syncfusion.Core.XForms.Android => 0xbd0daab1e651e52b => 89
	i64 13629449975987305271, ; 410: Microsoft.AppCenter.Android.Bindings => 0xbd25888a8eadfb37 => 34
	i64 13643785327914841093, ; 411: Plugin.Media.dll => 0xbd587677c60cf405 => 68
	i64 13647433987885684830, ; 412: Syncfusion.SfNumericTextBox.Android => 0xbd656ce79f84d85e => 107
	i64 13647894001087880694, ; 413: System.Data.dll => 0xbd670f48cb071df6 => 11
	i64 13828521679616088467, ; 414: Xamarin.Kotlin.StdLib.Common => 0xbfe8c733724e1993 => 246
	i64 13849821837823696015, ; 415: Syncfusion.Expander.XForms.Android => 0xc0347394fdeddc8f => 92
	i64 13852575513600495870, ; 416: ImageCircle.Forms.Plugin.dll => 0xc03e3c09186e90fe => 25
	i64 13959074834287824816, ; 417: Xamarin.AndroidX.Fragment => 0xc1b8989a7ad20fb0 => 189
	i64 13967638549803255703, ; 418: Xamarin.Forms.Platform.Android => 0xc1d70541e0134797 => 236
	i64 13970307180132182141, ; 419: Syncfusion.Licensing => 0xc1e0805ccade287d => 95
	i64 13978300845450874167, ; 420: GeoTimeZone.dll => 0xc1fce68f08a44d37 => 24
	i64 13987974187833695008, ; 421: Xamarin.AndroidX.Lifecycle.Service.dll => 0xc21f446991291b20 => 199
	i64 13996611348223281261, ; 422: Octane.Xamarin.Forms.VideoPlayer.dll => 0xc23df3dd2e0cc86d => 60
	i64 14061024831517255851, ; 423: Syncfusion.SfComboBox.XForms.Android => 0xc322cb95f48868ab => 103
	i64 14124974489674258913, ; 424: Xamarin.AndroidX.CardView => 0xc405fd76067d19e1 => 174
	i64 14125464355221830302, ; 425: System.Threading.dll => 0xc407bafdbc707a9e => 6
	i64 14172845254133543601, ; 426: Xamarin.AndroidX.MultiDex => 0xc4b00faaed35f2b1 => 205
	i64 14261073672896646636, ; 427: Xamarin.AndroidX.Print => 0xc5e982f274ae0dec => 210
	i64 14327695147300244862, ; 428: System.Reflection.dll => 0xc6d632d338eb4d7e => 4
	i64 14369828458497533121, ; 429: Xamarin.Android.Support.Vector.Drawable => 0xc76be2d9300b64c1 => 162
	i64 14400856865250966808, ; 430: Xamarin.Android.Support.Core.UI => 0xc7da1f051a877d18 => 146
	i64 14451964210230602902, ; 431: Syncfusion.Cards.XForms.Android => 0xc88fb0e121742c96 => 87
	i64 14486659737292545672, ; 432: Xamarin.AndroidX.Lifecycle.LiveData => 0xc90af44707469e88 => 196
	i64 14495724990987328804, ; 433: Xamarin.AndroidX.ResourceInspection.Annotation => 0xc92b2913e18d5d24 => 213
	i64 14536303476482288245, ; 434: Syncfusion.Expander.XForms => 0xc9bb52fec700aa75 => 93
	i64 14538127318538747197, ; 435: Syncfusion.Licensing.dll => 0xc9c1cdc518e77d3d => 95
	i64 14551742072151931844, ; 436: System.Text.Encodings.Web.dll => 0xc9f22c50f1b8fbc4 => 126
	i64 14604329626201521481, ; 437: Microsoft.AspNetCore.SignalR.Client => 0xcaad006b00747d49 => 43
	i64 14622198132100031719, ; 438: TimeZoneConverter => 0xcaec7bbabb33e4e7 => 131
	i64 14644440854989303794, ; 439: Xamarin.AndroidX.LocalBroadcastManager.dll => 0xcb3b815e37daeff2 => 203
	i64 14661790646341542033, ; 440: Xamarin.Android.Support.SwipeRefreshLayout => 0xcb7924e94e552091 => 159
	i64 14669215534098758659, ; 441: Microsoft.Extensions.DependencyInjection.dll => 0xcb9385ceb3993c03 => 49
	i64 14763643331770587208, ; 442: OxyPlot.Xamarin.Forms.dll => 0xcce2ff639cc01848 => 63
	i64 14792063746108907174, ; 443: Xamarin.Google.Guava.ListenableFuture.dll => 0xcd47f79af9c15ea6 => 240
	i64 14809184851036126845, ; 444: Microsoft.AspNetCore.SignalR.Client.Core => 0xcd84cb28db1abe7d => 42
	i64 14852515768018889994, ; 445: Xamarin.AndroidX.CursorAdapter.dll => 0xce1ebc6625a76d0a => 182
	i64 14954388675289411854, ; 446: ZXing.Net.Mobile.Forms.dll => 0xcf88a944b7bff10e => 256
	i64 14954917835170835695, ; 447: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xcf8a8a895a82ecef => 48
	i64 14987728460634540364, ; 448: System.IO.Compression.dll => 0xcfff1ba06622494c => 270
	i64 14988210264188246988, ; 449: Xamarin.AndroidX.DocumentFile => 0xd000d1d307cddbcc => 184
	i64 15076659072870671916, ; 450: System.ObjectModel.dll => 0xd13b0d8c1620662c => 261
	i64 15133485256822086103, ; 451: System.Linq.dll => 0xd204f0a9127dd9d7 => 263
	i64 15150743910298169673, ; 452: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xd2424150783c3149 => 211
	i64 15188640517174936311, ; 453: Xamarin.Android.Arch.Core.Common => 0xd2c8e413d75142f7 => 133
	i64 15246441518555807158, ; 454: Xamarin.Android.Arch.Core.Common.dll => 0xd3963dc832493db6 => 133
	i64 15279429628684179188, ; 455: Xamarin.KotlinX.Coroutines.Android.dll => 0xd40b704b1c4c96f4 => 250
	i64 15326820765897713587, ; 456: Xamarin.Android.Arch.Core.Runtime.dll => 0xd4b3ce481769e7b3 => 134
	i64 15370334346939861994, ; 457: Xamarin.AndroidX.Core.dll => 0xd54e65a72c560bea => 181
	i64 15377983283090003614, ; 458: Syncfusion.SfBusyIndicator.Android.dll => 0xd5699251e679969e => 98
	i64 15391712275433856905, ; 459: Microsoft.Extensions.DependencyInjection.Abstractions => 0xd59a58c406411f89 => 48
	i64 15398511348637731642, ; 460: FFImageLoading.Forms.Platform.dll => 0xd5b2807c9d5f8b3a => 21
	i64 15423352120420908645, ; 461: Syncfusion.SfComboBox.XForms.Android.dll => 0xd60ac1097f74be65 => 103
	i64 15457813392950723921, ; 462: Xamarin.Android.Support.Media.Compat => 0xd6852f61c31a8551 => 156
	i64 15526743539506359484, ; 463: System.Text.Encoding.dll => 0xd77a12fc26de2cbc => 9
	i64 15568534730848034786, ; 464: Xamarin.Android.Support.VersionedParcelable.dll => 0xd80e8bda21875fe2 => 163
	i64 15582737692548360875, ; 465: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xd841015ed86f6aab => 201
	i64 15609085926864131306, ; 466: System.dll => 0xd89e9cf3334914ea => 119
	i64 15661133872274321916, ; 467: System.Xml.ReaderWriter.dll => 0xd9578647d4bfb1fc => 274
	i64 15721124167271087156, ; 468: Plugin.DeviceInfo.dll => 0xda2ca722d4007c34 => 66
	i64 15777549416145007739, ; 469: Xamarin.AndroidX.SlidingPaneLayout.dll => 0xdaf51d99d77eb47b => 217
	i64 15810740023422282496, ; 470: Xamarin.Forms.Xaml => 0xdb6b08484c22eb00 => 238
	i64 15817206913877585035, ; 471: System.Threading.Tasks.dll => 0xdb8201e29086ac8b => 262
	i64 15847085070278954535, ; 472: System.Threading.Channels.dll => 0xdbec27e8f35f8e27 => 128
	i64 15851975962649584118, ; 473: zxing.portable.dll => 0xdbfd882691c261f6 => 257
	i64 15852824340364052161, ; 474: Microsoft.AspNetCore.Http.Features.dll => 0xdc008bbee610c6c1 => 41
	i64 15891335270217393935, ; 475: Retail.dll => 0xdc895d3b7bf1a70f => 70
	i64 15930129725311349754, ; 476: Xamarin.GooglePlayServices.Tasks.dll => 0xdd1330956f12f3fa => 244
	i64 15963349826457351533, ; 477: System.Threading.Tasks.Extensions => 0xdd893616f748b56d => 279
	i64 16106398470792018018, ; 478: Syncfusion.Expander.XForms.Android.dll => 0xdf856c12e673f862 => 92
	i64 16107354805249926211, ; 479: ZXingNetMobile.dll => 0xdf88d1dade1a6443 => 258
	i64 16119456071779071829, ; 480: FastAndroidCamera.dll => 0xdfb3cfe48ae7b755 => 18
	i64 16150183177059685051, ; 481: Syncfusion.SfChart.XForms.dll => 0xe020fa083e21d2bb => 102
	i64 16154507427712707110, ; 482: System => 0xe03056ea4e39aa26 => 119
	i64 16156430004425724367, ; 483: Microsoft.AspNetCore.Http.Connections.Client.dll => 0xe0372b7d144211cf => 39
	i64 16242842420508142678, ; 484: Xamarin.Android.Support.CoordinaterLayout.dll => 0xe16a2b1f8908ac56 => 145
	i64 16321164108206115771, ; 485: Microsoft.Extensions.Logging.Abstractions.dll => 0xe2806c487e7b0bbb => 50
	i64 16324796876805858114, ; 486: SkiaSharp.dll => 0xe28d5444586b6342 => 76
	i64 16365568899075028762, ; 487: Retail.Infrastructure => 0xe31e2e34888c371a => 1
	i64 16423015068819898779, ; 488: Xamarin.Kotlin.StdLib.Jdk8 => 0xe3ea453135e5c19b => 249
	i64 16471792842892863418, ; 489: DLToolkit.Forms.Controls.FlowListView.dll => 0xe4979051be7367ba => 17
	i64 16496768397145114574, ; 490: Mono.Android.Export.dll => 0xe4f04b741db987ce => 55
	i64 16526376532108668976, ; 491: ZXing.Net.Mobile.Forms.Android => 0xe5597be53cb07030 => 255
	i64 16552427520763284698, ; 492: Syncfusion.SfChart.XForms.Android => 0xe5b60921b17eccda => 101
	i64 16565028646146589191, ; 493: System.ComponentModel.Composition.dll => 0xe5e2cdc9d3bcc207 => 272
	i64 16605226748660468415, ; 494: Microsoft.AspNetCore.SignalR.Common => 0xe6719dbfe8b8cabf => 44
	i64 16621146507174665210, ; 495: Xamarin.AndroidX.ConstraintLayout => 0xe6aa2caf87dedbfa => 178
	i64 16677317093839702854, ; 496: Xamarin.AndroidX.Navigation.UI => 0xe771bb8960dd8b46 => 208
	i64 16755018182064898362, ; 497: SQLitePCLRaw.core.dll => 0xe885c843c330813a => 81
	i64 16767985610513713374, ; 498: Xamarin.Android.Arch.Core.Runtime => 0xe8b3da12798808de => 134
	i64 16787552971463248837, ; 499: Syncfusion.SfPicker.XForms.Android.dll => 0xe8f95e7bb81ecbc5 => 111
	i64 16822611501064131242, ; 500: System.Data.DataSetExtensions => 0xe975ec07bb5412aa => 269
	i64 16833383113903931215, ; 501: mscorlib => 0xe99c30c1484d7f4f => 56
	i64 16866861824412579935, ; 502: System.Runtime.InteropServices.WindowsRuntime => 0xea132176ffb5785f => 13
	i64 16890310621557459193, ; 503: System.Text.RegularExpressions.dll => 0xea66700587f088f9 => 266
	i64 16895806301542741427, ; 504: Plugin.Permissions => 0xea79f6503d42f5b3 => 69
	i64 16932527889823454152, ; 505: Xamarin.Android.Support.Core.Utils.dll => 0xeafc6c67465253c8 => 147
	i64 17001062948826229159, ; 506: Microcharts.Forms.dll => 0xebefe8ad2cd7a9a7 => 31
	i64 17009591894298689098, ; 507: Xamarin.Android.Support.Animated.Vector.Drawable => 0xec0e35b50a097e4a => 140
	i64 17024911836938395553, ; 508: Xamarin.AndroidX.Annotation.Experimental.dll => 0xec44a31d250e5fa1 => 167
	i64 17031351772568316411, ; 509: Xamarin.AndroidX.Navigation.Common.dll => 0xec5b843380a769fb => 206
	i64 17037200463775726619, ; 510: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xec704b8e0a78fc1b => 192
	i64 17118171214553292978, ; 511: System.Threading.Channels => 0xed8ff6060fc420b2 => 128
	i64 17124705692820578889, ; 512: Lottie.Forms => 0xeda72d18d7ae2249 => 28
	i64 17211698044874985152, ; 513: OxyPlot.Xamarin.Android => 0xeedc3c2e2a0f0ec0 => 62
	i64 17285063141349522879, ; 514: Rg.Plugins.Popup => 0xefe0e158cc55fdbf => 71
	i64 17333249706306540043, ; 515: System.Diagnostics.Tracing.dll => 0xf08c12c5bb8b920b => 275
	i64 17383232329670771222, ; 516: Xamarin.Android.Support.CustomView.dll => 0xf13da5b41a1cc216 => 149
	i64 17428701562824544279, ; 517: Xamarin.Android.Support.Core.UI.dll => 0xf1df2fbaec73d017 => 146
	i64 17483646997724851973, ; 518: Xamarin.Android.Support.ViewPager => 0xf2a2644fe5b6ef05 => 164
	i64 17524135665394030571, ; 519: Xamarin.Android.Support.Print => 0xf3323c8a739097eb => 157
	i64 17544493274320527064, ; 520: Xamarin.AndroidX.AsyncLayoutInflater => 0xf37a8fada41aded8 => 172
	i64 17571845317586269034, ; 521: Microsoft.AspNetCore.Connections.Abstractions.dll => 0xf3dbbc377ad7336a => 38
	i64 17627500474728259406, ; 522: System.Globalization => 0xf4a176498a351f4e => 267
	i64 17636563193350668017, ; 523: Microsoft.AspNetCore.Http.Connections.Common => 0xf4c1a8c826653ef1 => 40
	i64 17643123953373031521, ; 524: FFImageLoading => 0xf4d8f7c220fc2c61 => 19
	i64 17666959971718154066, ; 525: Xamarin.Android.Support.CustomView => 0xf52da67d9f4e4752 => 149
	i64 17671790519499593115, ; 526: SkiaSharp.Views.Android => 0xf53ecfd92be3959b => 77
	i64 17685921127322830888, ; 527: System.Diagnostics.Debug.dll => 0xf571038fafa74828 => 5
	i64 17704177640604968747, ; 528: Xamarin.AndroidX.Loader => 0xf5b1dfc36cac272b => 202
	i64 17710060891934109755, ; 529: Xamarin.AndroidX.Lifecycle.ViewModel => 0xf5c6c68c9e45303b => 200
	i64 17712670374920797664, ; 530: System.Runtime.InteropServices.dll => 0xf5d00bdc38bd3de0 => 276
	i64 17760961058993581169, ; 531: Xamarin.Android.Arch.Lifecycle.Common => 0xf67b9bfb46dbac71 => 135
	i64 17816041456001989629, ; 532: Xamarin.Forms.Maps.dll => 0xf73f4b4f90a1bbfd => 235
	i64 17832140366245679051, ; 533: Syncfusion.Data.Portable => 0xf7787d2f32fa4fcb => 91
	i64 17838668724098252521, ; 534: System.Buffers.dll => 0xf78faeb0f5bf3ee9 => 116
	i64 17841643939744178149, ; 535: Xamarin.Android.Arch.Lifecycle.ViewModel => 0xf79a40a25573dfe5 => 139
	i64 17865949717230441556, ; 536: Xamarin.AndroidX.Room.Runtime => 0xf7f09a9c2682d454 => 215
	i64 17882897186074144999, ; 537: FormsViewGroup => 0xf82cd03e3ac830e7 => 23
	i64 17891337867145587222, ; 538: Xamarin.Jetbrains.Annotations => 0xf84accff6fb52a16 => 245
	i64 17892495832318972303, ; 539: Xamarin.Forms.Xaml.dll => 0xf84eea293687918f => 238
	i64 17898189338132230373, ; 540: Syncfusion.SfRadialMenu.XForms.dll => 0xf863245fd60e30e5 => 115
	i64 17928294245072900555, ; 541: System.IO.Compression.FileSystem.dll => 0xf8ce18a0b24011cb => 271
	i64 17947624217716767869, ; 542: FFImageLoading.Platform => 0xf912c522ab34bc7d => 22
	i64 17956840076609788800, ; 543: OxyPlot.Xamarin.Forms.Platform.Android.dll => 0xf93382e906d31b80 => 64
	i64 17958105683855786126, ; 544: Xamarin.Android.Arch.Lifecycle.LiveData.Core.dll => 0xf93801f92d25c08e => 136
	i64 17969331831154222830, ; 545: Xamarin.GooglePlayServices.Maps => 0xf95fe418471126ee => 243
	i64 17986907704309214542, ; 546: Xamarin.GooglePlayServices.Basement.dll => 0xf99e554223166d4e => 242
	i64 18025913125965088385, ; 547: System.Threading => 0xfa28e87b91334681 => 6
	i64 18032536838832924778, ; 548: GeoTimeZone => 0xfa4070b6e5c8286a => 24
	i64 18116111925905154859, ; 549: Xamarin.AndroidX.Arch.Core.Runtime => 0xfb695bd036cb632b => 171
	i64 18121036031235206392, ; 550: Xamarin.AndroidX.Navigation.Common => 0xfb7ada42d3d42cf8 => 206
	i64 18129453464017766560, ; 551: System.ServiceModel.Internals.dll => 0xfb98c1df1ec108a0 => 260
	i64 18219918163864392437, ; 552: Syncfusion.SfDataGrid.XForms.dll => 0xfcda270969d312f5 => 106
	i64 18245806341561545090, ; 553: System.Collections.Concurrent.dll => 0xfd3620327d587182 => 12
	i64 18260797123374478311, ; 554: Xamarin.AndroidX.Emoji2 => 0xfd6b623bde35f3e7 => 187
	i64 18273040030599805019, ; 555: Shiny.Notifications.dll => 0xfd96e117d664f85b => 73
	i64 18301997741680159453, ; 556: Xamarin.Android.Support.CursorAdapter => 0xfdfdc1fa58d8eadd => 148
	i64 18305135509493619199, ; 557: Xamarin.AndroidX.Navigation.Runtime.dll => 0xfe08e7c2d8c199ff => 207
	i64 18370042311372477656, ; 558: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0xfeef80274e4094d8 => 82
	i64 18380184030268848184 ; 559: Xamarin.AndroidX.VersionedParcelable => 0xff1387fe3e7b7838 => 226
], align 8
@assembly_image_cache_indices = local_unnamed_addr constant [560 x i32] [
	i32 21, i32 186, i32 86, i32 53, i32 54, i32 30, i32 175, i32 88, ; 0..7
	i32 216, i32 71, i32 217, i32 180, i32 25, i32 233, i32 123, i32 33, ; 8..15
	i32 139, i32 64, i32 198, i32 241, i32 10, i32 191, i32 99, i32 143, ; 16..23
	i32 32, i32 136, i32 82, i32 185, i32 31, i32 259, i32 237, i32 278, ; 24..31
	i32 0, i32 72, i32 239, i32 93, i32 81, i32 66, i32 170, i32 152, ; 32..39
	i32 69, i32 29, i32 15, i32 104, i32 75, i32 265, i32 163, i32 83, ; 40..47
	i32 59, i32 59, i32 168, i32 201, i32 192, i32 58, i32 268, i32 161, ; 48..55
	i32 132, i32 169, i32 216, i32 46, i32 166, i32 160, i32 200, i32 22, ; 56..63
	i32 230, i32 279, i32 248, i32 90, i32 94, i32 58, i32 205, i32 173, ; 64..71
	i32 34, i32 185, i32 273, i32 12, i32 213, i32 195, i32 126, i32 124, ; 72..79
	i32 89, i32 179, i32 224, i32 125, i32 27, i32 165, i32 129, i32 56, ; 80..87
	i32 49, i32 209, i32 36, i32 124, i32 220, i32 65, i32 20, i32 158, ; 88..95
	i32 232, i32 151, i32 131, i32 239, i32 156, i32 29, i32 84, i32 101, ; 96..103
	i32 28, i32 194, i32 167, i32 72, i32 122, i32 229, i32 251, i32 242, ; 104..111
	i32 222, i32 221, i32 121, i32 125, i32 65, i32 274, i32 57, i32 212, ; 112..119
	i32 94, i32 100, i32 186, i32 44, i32 51, i32 108, i32 86, i32 35, ; 120..127
	i32 268, i32 180, i32 244, i32 214, i32 164, i32 257, i32 254, i32 67, ; 128..135
	i32 80, i32 150, i32 142, i32 132, i32 260, i32 226, i32 233, i32 197, ; 136..143
	i32 172, i32 165, i32 221, i32 145, i32 112, i32 155, i32 113, i32 2, ; 144..151
	i32 2, i32 236, i32 115, i32 157, i32 68, i32 231, i32 203, i32 18, ; 152..159
	i32 234, i32 61, i32 153, i32 204, i32 224, i32 223, i32 140, i32 5, ; 160..167
	i32 178, i32 117, i32 218, i32 107, i32 37, i32 228, i32 176, i32 199, ; 168..175
	i32 141, i32 254, i32 98, i32 209, i32 75, i32 110, i32 70, i32 196, ; 176..183
	i32 40, i32 43, i32 159, i32 113, i32 237, i32 23, i32 80, i32 127, ; 184..191
	i32 155, i32 198, i32 195, i32 162, i32 173, i32 183, i32 52, i32 160, ; 192..199
	i32 273, i32 191, i32 76, i32 109, i32 38, i32 130, i32 88, i32 122, ; 200..207
	i32 45, i32 90, i32 193, i32 190, i32 252, i32 243, i32 13, i32 148, ; 208..215
	i32 263, i32 97, i32 33, i32 4, i32 219, i32 17, i32 130, i32 261, ; 216..223
	i32 106, i32 123, i32 232, i32 231, i32 14, i32 47, i32 247, i32 110, ; 224..231
	i32 11, i32 142, i32 168, i32 218, i32 16, i32 161, i32 77, i32 105, ; 232..239
	i32 272, i32 3, i32 105, i32 194, i32 118, i32 141, i32 269, i32 8, ; 240..247
	i32 26, i32 3, i32 78, i32 250, i32 153, i32 45, i32 7, i32 63, ; 248..255
	i32 138, i32 177, i32 208, i32 197, i32 85, i32 188, i32 223, i32 84, ; 256..263
	i32 26, i32 47, i32 207, i32 271, i32 118, i32 154, i32 246, i32 60, ; 264..271
	i32 150, i32 211, i32 102, i32 62, i32 19, i32 20, i32 219, i32 96, ; 272..279
	i32 228, i32 169, i32 108, i32 264, i32 111, i32 266, i32 121, i32 179, ; 280..287
	i32 204, i32 96, i32 16, i32 54, i32 176, i32 259, i32 135, i32 241, ; 288..295
	i32 188, i32 278, i32 14, i32 248, i32 183, i32 154, i32 245, i32 9, ; 296..303
	i32 143, i32 222, i32 251, i32 215, i32 181, i32 46, i32 267, i32 0, ; 304..311
	i32 151, i32 229, i32 7, i32 235, i32 265, i32 170, i32 214, i32 138, ; 312..319
	i32 87, i32 275, i32 50, i32 117, i32 109, i32 129, i32 41, i32 230, ; 320..327
	i32 227, i32 253, i32 52, i32 182, i32 144, i32 210, i32 253, i32 277, ; 328..335
	i32 234, i32 55, i32 35, i32 104, i32 276, i32 120, i32 227, i32 51, ; 336..343
	i32 193, i32 166, i32 220, i32 57, i32 67, i32 252, i32 190, i32 258, ; 344..351
	i32 79, i32 262, i32 99, i32 79, i32 158, i32 36, i32 137, i32 97, ; 352..359
	i32 74, i32 256, i32 116, i32 8, i32 225, i32 127, i32 32, i32 37, ; 360..367
	i32 112, i32 83, i32 120, i32 137, i32 144, i32 74, i32 189, i32 202, ; 368..375
	i32 184, i32 27, i32 174, i32 270, i32 255, i32 175, i32 264, i32 249, ; 376..383
	i32 100, i32 53, i32 277, i32 147, i32 10, i32 61, i32 91, i32 187, ; 384..391
	i32 39, i32 1, i32 73, i32 152, i32 114, i32 15, i32 85, i32 225, ; 392..399
	i32 30, i32 177, i32 114, i32 42, i32 240, i32 247, i32 171, i32 78, ; 400..407
	i32 212, i32 89, i32 34, i32 68, i32 107, i32 11, i32 246, i32 92, ; 408..415
	i32 25, i32 189, i32 236, i32 95, i32 24, i32 199, i32 60, i32 103, ; 416..423
	i32 174, i32 6, i32 205, i32 210, i32 4, i32 162, i32 146, i32 87, ; 424..431
	i32 196, i32 213, i32 93, i32 95, i32 126, i32 43, i32 131, i32 203, ; 432..439
	i32 159, i32 49, i32 63, i32 240, i32 42, i32 182, i32 256, i32 48, ; 440..447
	i32 270, i32 184, i32 261, i32 263, i32 211, i32 133, i32 133, i32 250, ; 448..455
	i32 134, i32 181, i32 98, i32 48, i32 21, i32 103, i32 156, i32 9, ; 456..463
	i32 163, i32 201, i32 119, i32 274, i32 66, i32 217, i32 238, i32 262, ; 464..471
	i32 128, i32 257, i32 41, i32 70, i32 244, i32 279, i32 92, i32 258, ; 472..479
	i32 18, i32 102, i32 119, i32 39, i32 145, i32 50, i32 76, i32 1, ; 480..487
	i32 249, i32 17, i32 55, i32 255, i32 101, i32 272, i32 44, i32 178, ; 488..495
	i32 208, i32 81, i32 134, i32 111, i32 269, i32 56, i32 13, i32 266, ; 496..503
	i32 69, i32 147, i32 31, i32 140, i32 167, i32 206, i32 192, i32 128, ; 504..511
	i32 28, i32 62, i32 71, i32 275, i32 149, i32 146, i32 164, i32 157, ; 512..519
	i32 172, i32 38, i32 267, i32 40, i32 19, i32 149, i32 77, i32 5, ; 520..527
	i32 202, i32 200, i32 276, i32 135, i32 235, i32 91, i32 116, i32 139, ; 528..535
	i32 215, i32 23, i32 245, i32 238, i32 115, i32 271, i32 22, i32 64, ; 536..543
	i32 136, i32 243, i32 242, i32 6, i32 24, i32 171, i32 206, i32 260, ; 544..551
	i32 106, i32 12, i32 187, i32 73, i32 148, i32 207, i32 82, i32 226 ; 560..559
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 8; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 8

; Function attributes: "frame-pointer"="non-leaf" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+neon,+outline-atomics" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 8
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 8
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="non-leaf" "target-cpu"="generic" "target-features"="+neon,+outline-atomics" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="non-leaf" "target-cpu"="generic" "target-features"="+neon,+outline-atomics" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2, !3, !4, !5}
!llvm.ident = !{!6}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"branch-target-enforcement", i32 0}
!3 = !{i32 1, !"sign-return-address", i32 0}
!4 = !{i32 1, !"sign-return-address-all", i32 0}
!5 = !{i32 1, !"sign-return-address-with-bkey", i32 0}
!6 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}
