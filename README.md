Sample repo to illustrate an crash with MAUI-iOS when a CollectionView is on a non-visibile Page and has updates to the groups it's bound to.  Scenario mimics logic our company uses in a real world application.

Currently using Microsoft.Maui.Controls v9.0.40.

To reproduce error:
1. Run application - should see a 'TODO' group with 'Mow Lawn', and a 'DONE' group with 2 other rows.
2. For the 'Mow Lawn' row, tap 'Details'
3. Within the DetailPage, tap the 'ACTIVE' button - work item will now be ACTIVE, and the groups will be updated in MainViewModel&MainPage.
4. Staying on the DetailPage, tap the 'TODO' button

Running the above on WinUI and Android works flawlessly.  Running on iOS results in a crash:

```
2025-02-14 11:23:37.989228+0000 MauiCollectionViewGroupChanging.iOS[17426:2337379] [general] *** Assertion failure in -[Microsoft_Maui_Controls_Handlers_Items_MauiCollectionView _validateSortedDeleteItems:moveItems:movedSourceIndexPaths:movedDestinationIndexPaths:insertItems:oldCollectionViewData:], UICollectionView.m:10861
**ObjCRuntime.ObjCException:** 'Loading...'

**ObjCRuntime.ObjCException:** 'Objective-C exception thrown.  Name: NSInternalInconsistencyException Reason: attempt to delete item 0 from section 0 which only contains 0 items before the update
Native stack trace:
	0   CoreFoundation                      0x00000001804b910c __exceptionPreprocess + 172
	1   libobjc.A.dylib                     0x0000000180092da8 objc_exception_throw + 72
	2   Foundation                          0x0000000180e67c70 _userInfoForFileAndLine + 0
	3   UIKitCore                           0x0000000185246f50 -[UICollectionView _endItemAnimationsWithInvalidationContext:tentativelyForReordering:animator:collectionViewAnimator:] + 4100
	4   UIKitCore                           0x000000018524518c -[UICollectionView _updateRowsAtIndexPaths:updateAction:updates:] + 436
	5   UIKitCore                           0x000000018524532c -[UICollectionView deleteItemsAtIndexPaths:] + 48
	6   UIKit                               0x00000001cbee586c -[UICollectionViewAccessibility deleteItemsAtIndexPaths:] + 92
	7   libxamarin-dotnet-debug.dylib       0x0000000105792d4c xamarin_dyn_objc_msgSendSuper + 164
	8   libmonosgen-2.0.dylib               0x0000000105aff668 do_icall + 200
	9   libmonosgen-2.0.dylib               0x0000000105afdc00 do_icall_wrapper + 356
	10  libmonosgen-2.0.dylib               0x0000000105af32f4 mono_interp_exec_method + 2824
	11  libmonosgen-2.0.dylib               0x0000000105af0dbc interp_runtime_invoke + 244
	12  libmonosgen-2.0.dylib               0x0000000105a0c914 mono_jit_runtime_invoke + 1244
	13  libmonosgen-2.0.dylib               0x0000000105bcb894 mono_runtime_try_invoke + 156
	14  libmonosgen-2.0.dylib               0x0000000105bcda30 mono_runtime_invoke + 484
	15  MauiCollectionViewGroupChanging.iOS 0x00000001044337c4 _ZL30native_to_managed_trampoline_9P11objc_objectP13objc_selectorPP11_MonoMethodj + 316
	16  MauiCollectionViewGroupChanging.iOS 0x00000001044863f4 -[UIKit_UIControlEventProxy BridgeSelector] + 44
	17  UIKitCore                           0x0000000185b36e3c -[UIApplication sendAction:to:from:forEvent:] + 96
	18  UIKitCore                           0x000000018540c830 -[UIControl sendAction:to:forEvent:] + 108
	19  UIKitCore                           0x000000018540cb74 -[UIControl _sendActionsForEvents:withEvent:] + 268
	20  UIKitCore                           0x00000001854095ec -[UIButton _sendActionsForEvents:withEvent:] + 120
	21  UIKitCore                           0x000000018540b80c -[UIControl touchesEnded:withEvent:] + 392
	22  UIKitCore                           0x0000000185b6aa10 -[UIWindow _sendTouchesForEvent:] + 972
	23  UIKitCore                           0x0000000185b6be20 -[UIWindow sendEvent:] + 2840
	24  UIKitCore                           0x0000000185b4b80c -[UIApplication sendEvent:] + 376
	25  UIKit                               0x00000001cbedd2fc -[UIApplicationAccessibility sendEvent:] + 108
	26  UIKitCore                           0x0000000185bd5c70 __dispatchPreprocessedEventFromEventQueue + 1156
	27  UIKitCore                           0x0000000185bd8c00 __processEventQueue + 5592
	28  UIKitCore                           0x0000000185bd0f10 updateCycleEntry + 156
	29  UIKitCore                           0x00000001850a5cec _UIUpdateSequenceRun + 76
	30  UIKitCore                           0x0000000185a60858 schedulerStepScheduledMainSection + 168
	31  UIKitCore                           0x0000000185a5fc90 runloopSourceCallback + 80
	32  CoreFoundation                      0x000000018041d294 __CFRUNLOOP_IS_CALLING_OUT_TO_A_SOURCE0_PERFORM_FUNCTION__ + 24
	33  CoreFoundation                      0x000000018041d1dc __CFRunLoopDoSource0 + 172
	34  CoreFoundation                      0x000000018041c940 __CFRunLoopDoSources0 + 232
	35  CoreFoundation                      0x0000000180416e84 __CFRunLoopRun + 788
	36  CoreFoundation                      0x00000001804166f4 CFRunLoopRunSpecific + 552
	37  GraphicsServices                    0x00000001905e5b10 GSEventRunModal + 160
	38  UIKitCore                           0x0000000185b319dc -[UIApplication _run] + 796
	39  UIKitCore                           0x0000000185b35bd4 UIApplicationMain + 124
	40  libxamarin-dotnet-debug.dylib       0x0000000105751288 xamarin_UIApplicationMain + 60
	41  libmonosgen-2.0.dylib               0x0000000105aff6dc do_icall + 316
	42  libmonosgen-2.0.dylib               0x0000000105afdc00 do_icall_wrapper + 356
	43  libmonosgen-2.0.dylib               0x0000000105af32f4 mono_interp_exec_method + 2824
	44  libmonosgen-2.0.dylib               0x0000000105af0dbc interp_runtime_invoke + 244
	45  libmonosgen-2.0.dylib               0x0000000105a0c914 mono_jit_runtime_invoke + 1244
	46  libmonosgen-2.0.dylib               0x0000000105bc9e90 mono_runtime_invoke_checked + 148
	47  libmonosgen-2.0.dylib               0x0000000105bd10f0 mono_runtime_exec_main_checked + 116
	48  libmonosgen-2.0.dylib               0x0000000105a5f9a4 mono_jit_exec + 364
	49  libxamarin-dotnet-debug.dylib       0x0000000105791bac xamarin_main + 2312
	50  MauiCollectionViewGroupChanging.iOS 0x00000001044ded50 main + 64
	51  dyld                                0x0000000105115410 start_sim + 20
	52  ???                                 0x0000000105262274 0x0 + 4381352564
'

2025-02-14 11:23:47.058524+0000 MauiCollectionViewGroupChanging.iOS[17426:2337379] 
Unhandled Exception:
ObjCRuntime.ObjCException: Objective-C exception thrown.  Name: NSInternalInconsistencyException Reason: attempt to delete item 0 from section 0 which only contains 0 items before the update
Native stack trace:
	0   CoreFoundation                      0x00000001804b910c __exceptionPreprocess + 172
	1   libobjc.A.dylib                     0x0000000180092da8 objc_exception_throw + 72
	2   Foundation                          0x0000000180e67c70 _userInfoForFileAndLine + 0
	3   UIKitCore                           0x0000000185246f50 -[UICollectionView _endItemAnimationsWithInvalidationContext:tentativelyForReordering:animator:collectionViewAnimator:] + 4100
	4   UIKitCore                           0x000000018524518c -[UICollectionView _updateRowsAtIndexPaths:updateAction:updates:] + 436
	5   UIKitCore                           0x000000018524532c -[UICollectionView deleteItemsAtIndexPaths:] + 48
	6   UIKit                               0x00000001cbee586c -[UICollectionViewAccessibility deleteItemsAtIndexPaths:] + 92
	7   libxamarin-dotnet-debug.dylib       0x0000000105792d4c xamarin_dyn_objc_msgSendSuper + 164
	8   libmonosgen-2.0.dylib               0x0000000105aff668 do_icall + 200
	9   libmonosgen-2.0.dylib               0x0000000105afdc00 do_icall_wrapper + 356
	10  libmonosgen-2.0.dylib               0x0000000105af32f4 mono_interp_exec_method + 2824
	11  libmonosgen-2.0.dylib               0x0000000105af0dbc interp_runtime_invoke + 244
	12  libmonosgen-2.0.dylib               0x0000000105a0c914 mono_jit_runtime_invoke + 1244
	13  libmonosgen-2.0.dylib               0x0000000105bcb894 mono_runtime_try_invoke + 156
	14  libmonosgen-2.0.dylib               0x0000000105bcda30 mono_runtime_invoke + 484
	15  MauiCollectionViewGroupChanging.iOS 0x00000001044337c4 _ZL30native_to_managed_trampoline_9P11objc_objectP13objc_selectorPP11_MonoMethodj + 316
	16  MauiCollectionViewGroupChanging.iOS 0x00000001044863f4 -[UIKit_UIControlEventProxy BridgeSelector] + 44
	17  UIKitCore                           0x0000000185b36e3c -[UIApplication sendAction:to:from:forEvent:] + 96
	18  UIKitCore                           0x000000018540c830 -[UIControl sendAction:to:forEvent:] + 108
	19  UIKitCore                           0x000000018540cb74 -[UIControl _sendActionsForEvents:withEvent:] + 268
	20  UIKitCore                           0x00000001854095ec -[UIButton _sendActionsForEvents:withEvent:] + 120
	21  UIKitCore                           0x000000018540b80c -[UIControl touchesEnded:withEvent:] + 392
	22  UIKitCore                           0x0000000185b6aa10 -[UIWindow _sendTouchesForEvent:] + 972
	23  UIKitCore                           0x0000000185b6be20 -[UIWindow sendEvent:] + 2840
	24  UIKitCore                           0x0000000185b4b80c -[UIApplication sendEvent:] + 376
	25  UIKit                               0x00000001cbedd2fc -[UIApplicationAccessibility sendEvent:] + 108
	26  UIKitCore                           0x0000000185bd5c70 __dispatchPreprocessedEventFromEventQueue + 1156
	27  UIKitCore                           0x0000000185bd8c00 __processEventQueue + 5592
	28  UIKitCore                           0x0000000185bd0f10 updateCycleEntry + 156
	29  UIKitCore                           0x00000001850a5cec _UIUpdateSequenceRun + 76
	30  UIKitCore                           0x0000000185a60858 schedulerStepScheduledMainSection + 168
	31  UIKitCore                           0x0000000185a5fc90 runloopSourceCallback + 80
	32  CoreFoundation                      0x000000018041d294 __CFRUNLOOP_IS_CALLING_OUT_TO_A_SOURCE0_PERFORM_FUNCTION__ + 24
	33  CoreFoundation                      0x000000018041d1dc __CFRunLoopDoSource0 + 172
	34  CoreFoundation                      0x000000018041c940 __CFRunLoopDoSources0 + 232
	35  CoreFoundation                      0x0000000180416e84 __CFRunLoopRun + 788
	36  CoreFoundation                      0x00000001804166f4 CFRunLoopRunSpecific + 552
	37  GraphicsServices                    0x00000001905e5b10 GSEventRunModal + 160
	38  UIKitCore                           0x0000000185b319dc -[UIApplication _run] + 796
	39  UIKitCore                           0x0000000185b35bd4 UIApplicationMain + 124
	40  libxamarin-dotnet-debug.dylib       0x0000000105751288 xamarin_UIApplicationMain + 60
	41  libmonosgen-2.0.dylib               0x0000000105aff6dc do_icall + 316
	42  libmonosgen-2.0.dylib               0x0000000105afdc00 do_icall_wrapper + 356
	43  libmonosgen-2.0.dylib               0x0000000105af32f4 mono_interp_exec_method + 2824
	44  libmonosgen-2.0.dylib               0x0000000105af0dbc interp_runtime_invoke + 244
	45  libmonosgen-2.0.dylib               0x0000000105a0c914 mono_jit_runtime_invoke + 1244
	46  libmonosgen-2.0.dylib               0x0000000105bc9e90 mono_runtime_invoke_checked + 148
	47  libmonosgen-2.0.dylib               0x0000000105bd10f0 mono_runtime_exec_main_checked + 116
	48  libmonosgen-2.0.dylib               0x0000000105a5f9a4 mono_jit_exec + 364
	49  libxamarin-dotnet-debug.dylib       0x0000000105791bac xamarin_main + 2312
	50  MauiCollectionViewGroupChanging.iOS 0x00000001044ded50 main + 64
	51  dyld                                0x0000000105115410 start_sim + 20
	52  ???                                 0x0000000105262274 0x0 + 4381352564

   at ObjCRuntime.Runtime.ThrowException(IntPtr gchandle) in /Users/builder/azdo/_work/9/s/xamarin-macios/src/ObjCRuntime/Runtime.cs:line 2742
   at UIKit.UIApplication.UIApplicationMain(Int32 argc, String[] argv, IntPtr principalClassName, IntPtr delegateClassName) in /Users/builder/azdo/_work/9/s/xamarin-macios/src/UIKit/UIApplication.cs:line 64
   at UIKit.UIApplication.Main(String[] args, Type principalClass, Type delegateClass) in /Users/builder/azdo/_work/9/s/xamarin-macios/src/UIKit/UIApplication.cs:line 96
   at MauiCollectionViewGroupChanging.iOS.Program.Main(String[] args) in D:\Projects\Test\MauiCollectionViewGroupChanging\MauiCollectionViewGroupChanging.iOS\Main.cs:line 13
Native stack trace:
	0   CoreFoundation                      0x00000001804b910c __exceptionPreprocess + 172
	1   libobjc.A.dylib                     0x0000000180092da8 objc_exception_throw + 72
	2   Foundation                          0x0000000180e67c70 _userInfoForFileAndLine + 0
	3   UIKitCore                           0x0000000185246f50 -[UICollectionView _endItemAnimationsWithInvalidationContext:tentativelyForReordering:animator:collectionViewAnimator:] + 4100
	4   UIKitCore                           0x000000018524518c -[UICollectionView _updateRowsAtIndexPaths:updateAction:updates:] + 436
	5   UIKitCore                           0x000000018524532c -[UICollectionView deleteItemsAtIndexPaths:] + 48
	6   UIKit                               0x00000001cbee586c -[UICollectionViewAccessibility deleteItemsAtIndexPaths:] + 92
	7   libxamarin-dotnet-debug.dylib       0x0000000105792d4c xamarin_dyn_objc_msgSendSuper + 164
	8   libmonosgen-2.0.dylib               0x0000000105aff668 do_icall + 200
	9   libmonosgen-2.0.dylib               0x0000000105afdc00 do_icall_wrapper + 356
	10  libmonosgen-2.0.dylib               0x0000000105af32f4 mono_interp_exec_method + 2824
	11  libmonosgen-2.0.dylib               0x0000000105af0dbc interp_runtime_invoke + 244
	12  libmonosgen-2.0.dylib               0x0000000105a0c914 mono_jit_runtime_invoke + 1244
	13  libmonosgen-2.0.dylib               0x0000000105bcb894 mono_runtime_try_invoke + 156
	14  libmonosgen-2.0.dylib               0x0000000105bcda30 mono_runtime_invoke + 484
	15  MauiCollectionViewGroupChanging.iOS 0x00000001044337c4 _ZL30native_to_managed_trampoline_9P11objc_objectP13objc_selectorPP11_MonoMethodj + 316
	16  MauiCollectionViewGroupChanging.iOS 0x00000001044863f4 -[UIKit_UIControlEventProxy BridgeSelector] + 44
	17  UIKitCore                           0x0000000185b36e3c -[UIApplication sendAction:to:from:forEvent:] + 96
	18  UIKitCore                           0x000000018540c830 -[UIControl sendAction:to:forEvent:] + 108
	19  UIKitCore                           0x000000018540cb74 -[UIControl _sendActionsForEvents:withEvent:] + 268
	20  UIKitCore                           0x00000001854095ec -[UIButton _sendActionsForEvents:withEvent:] + 120
	21  UIKitCore                           0x000000018540b80c -[UIControl touchesEnded:withEvent:] + 392
	22  UIKitCore                           0x0000000185b6aa10 -[UIWindow _sendTouchesForEvent:] + 972
	23  UIKitCore                           0x0000000185b6be20 -[UIWindow sendEvent:] + 2840
	24  UIKitCore                           0x0000000185b4b80c -[UIApplication sendEvent:] + 376
	25  UIKit                               0x00000001cbedd2fc -[UIApplicationAccessibility sendEvent:] + 108
	26  UIKitCore                           0x0000000185bd5c70 __dispatchPreprocessedEventFromEventQueue + 1156
	27  UIKitCore                           0x0000000185bd8c00 __processEventQueue + 5592
	28  UIKitCore                           0x0000000185bd0f10 updateCycleEntry + 156
	29  UIKitCore                           0x00000001850a5cec _UIUpdateSequenceRun + 76
	30  UIKitCore                           0x0000000185a60858 schedulerStepScheduledMainSection + 168
	31  UIKitCore                           0x0000000185a5fc90 runloopSourceCallback + 80
	32  CoreFoundation                      0x000000018041d294 __CFRUNLOOP_IS_CALLING_OUT_TO_A_SOURCE0_PERFORM_FUNCTION__ + 24
	33  CoreFoundation                      0x000000018041d1dc __CFRunLoopDoSource0 + 172
	34  CoreFoundation                      0x000000018041c940 __CFRunLoopDoSources0 + 232
	35  CoreFoundation                      0x0000000180416e84 __CFRunLoopRun + 788
	36  CoreFoundation                      0x00000001804166f4 CFRunLoopRunSpecific + 552
	37  GraphicsServices                    0x00000001905e5b10 GSEventRunModal + 160
	38  UIKitCore                           0x0000000185b319dc -[UIApplication _run] + 796
	39  UIKitCore                           0x0000000185b35bd4 UIApplicationMain + 124
	40  libxamarin-dotnet-debug.dylib       0x0000000105751288 xamarin_UIApplicationMain + 60
	41  libmonosgen-2.0.dylib               0x0000000105aff6dc do_icall + 316
	42  libmonosgen-2.0.dylib               0x0000000105afdc00 do_icall_wrapper + 356
	43  libmonosgen-2.0.dylib               0x0000000105af32f4 mono_interp_exec_method + 2824
	44  libmonosgen-2.0.dylib               0x0000000105af0dbc interp_runtime_invoke + 244
	45  libmonosgen-2.0.dylib               0x0000000105a0c914 mono_jit_runtime_invoke + 1244
	46  libmonosgen-2.0.dylib               0x0000000105bc9e90 mono_runtime_invoke_checked + 148
	47  libmonosgen-2.0.dylib               0x0000000105bd10f0 mono_runtime_exec_main_checked + 116
	48  libmonosgen-2.0.dylib               0x0000000105a5f9a4 mono_jit_exec + 364
	49  libxamarin-dotnet-debug.dylib       0x0000000105791bac xamarin_main + 2312
	50  MauiCollectionViewGroupChanging.iOS 0x00000001044ded50 main + 64
	51  dyld                                0x0000000105115410 start_sim + 20
	52  ???                                 0x0000000105262274 0x0 + 4381352564
2025-02-14 11:23:47.061997+0000 MauiCollectionViewGroupChanging.iOS[17426:2337379] Unhandled managed exception: Objective-C exception thrown.  Name: NSInternalInconsistencyException Reason: attempt to delete item 0 from section 0 which only contains 0 items before the update
Native stack trace:
	0   CoreFoundation                      0x00000001804b910c __exceptionPreprocess + 172
	1   libobjc.A.dylib                     0x0000000180092da8 objc_exception_throw + 72
	2   Foundation                          0x0000000180e67c70 _userInfoForFileAndLine + 0
	3   UIKitCore                           0x0000000185246f50 -[UICollectionView _endItemAnimationsWithInvalidationContext:tentativelyForReordering:animator:collectionViewAnimator:] + 4100
	4   UIKitCore                           0x000000018524518c -[UICollectionView _updateRowsAtIndexPaths:updateAction:updates:] + 436
	5   UIKitCore                           0x000000018524532c -[UICollectionView deleteItemsAtIndexPaths:] + 48
	6   UIKit                               0x00000001cbee586c -[UICollectionViewAccessibility deleteItemsAtIndexPaths:] + 92
	7   libxamarin-dotnet-debug.dylib       0x0000000105792d4c xamarin_dyn_objc_msgSendSuper + 164
	8   libmonosgen-2.0.dylib               0x0000000105aff668 do_icall + 200
	9   libmonosgen-2.0.dylib               0x0000000105afdc00 do_icall_wrapper + 356
	10  libmonosgen-2.0.dylib               0x0000000105af32f4 mono_interp_exec_method + 2824
	11  libmonosgen-2.0.dylib               0x0000000105af0dbc interp_runtime_invoke + 244
	12  libmonosgen-2.0.dylib               0x0000000105a0c914 mono_jit_runtime_invoke + 1244
	13  libmonosgen-2.0.dylib               0x0000000105bcb894 mono_runtime_try_invoke + 156
	14  libmonosgen-2.0.dylib               0x0000000105bcda30 mono_runtime_invoke + 484
	15  MauiCollectionViewGroupChanging.iOS 0x00000001044337c4 _ZL30native_to_managed_trampoline_9P11objc_objectP13objc_selectorPP11_MonoMethodj + 316
	16  MauiCollectionViewGroupChanging.iOS 0x00000001044863f4 -[UIKit_UIControlEventProxy BridgeSelector] + 44
	17  UIKitCore                           0x0000000185b36e3c -[UIApplication sendAction:to:from:forEvent:] + 96
	18  UIKitCore                           0x000000018540c830 -[UIControl sendAction:to:forEvent:] + 108
	19  UIKitCore                           0x000000018540cb74 -[UIControl _sendActionsForEvents:withEvent:] + 268
	20  UIKitCore                           0x00000001854095ec -[UIButton _sendActionsForEvents:withEvent:] + 120
	21  UIKitCore                           0x000000018540b80c -[UIControl touchesEnded:withEvent:] + 392
	22  UIKitCore                           0x0000000185b6aa10 -[UIWindow _sendTouchesForEvent:] + 972
	23  UIKitCore                           0x0000000185b6be20 -[UIWindow sendEvent:] + 2840
	24  UIKitCore                           0x0000000185b4b80c -[UIApplication sendEvent:] + 376
	25  UIKit                               0x00000001cbedd2fc -[UIApplicationAccessibility sendEvent:] + 108
	26  UIKitCore                           0x0000000185bd5c70 __dispatchPreprocessedEventFromEventQueue + 1156
	27  UIKitCore                           0x0000000185bd8c00 __processEventQueue + 5592
	28  UIKitCore                           0x0000000185bd0f10 updateCycleEntry + 156
	29  UIKitCore                           0x00000001850a5cec _UIUpdateSequenceRun + 76
	30  UIKitCore                           0x0000000185a60858 schedulerStepScheduledMainSection + 168
	31  UIKitCore                           0x0000000185a5fc90 runloopSourceCallback + 80
	32  CoreFoundation                      0x000000018041d294 __CFRUNLOOP_IS_CALLING_OUT_TO_A_SOURCE0_PERFORM_FUNCTION__ + 24
	33  CoreFoundation                      0x000000018041d1dc __CFRunLoopDoSource0 + 172
	34  CoreFoundation                      0x000000018041c940 __CFRunLoopDoSources0 + 232
	35  CoreFoundation                      0x0000000180416e84 __CFRunLoopRun + 788
	36  CoreFoundation                      0x00000001804166f4 CFRunLoopRunSpecific + 552
	37  GraphicsServices                    0x00000001905e5b10 GSEventRunModal + 160
	38  UIKitCore                           0x0000000185b319dc -[UIApplication _run] + 796
	39  UIKitCore                           0x0000000185b35bd4 UIApplicationMain + 124
	40  libxamarin-dotnet-debug.dylib       0x0000000105751288 xamarin_UIApplicationMain + 60
	41  libmonosgen-2.0.dylib               0x0000000105aff6dc do_icall + 316
	42  libmonosgen-2.0.dylib               0x0000000105afdc00 do_icall_wrapper + 356
	43  libmonosgen-2.0.dylib               0x0000000105af32f4 mono_interp_exec_method + 2824
	44  libmonosgen-2.0.dylib               0x0000000105af0dbc interp_runtime_invoke + 244
	45  libmonosgen-2.0.dylib               0x0000000105a0c914 mono_jit_runtime_invoke + 1244
	46  libmonosgen-2.0.dylib               0x0000000105bc9e90 mono_runtime_invoke_checked + 148
	47  libmonosgen-2.0.dylib               0x0000000105bd10f0 mono_runtime_exec_main_checked + 116
	48  libmonosgen-2.0.dylib               0x0000000105a5f9a4 mono_jit_exec + 364
	49  libxamarin-dotnet-debug.dylib       0x0000000105791bac xamarin_main + 2312
	50  MauiCollectionViewGroupChanging.iOS 0x00000001044ded50 main + 64
	51  dyld                                0x0000000105115410 start_sim + 20
	52  ???                                 0x0000000105262274 0x0 + 4381352564
 (ObjCRuntime.ObjCException)
   at ObjCRuntime.Runtime.ThrowException(IntPtr gchandle) in /Users/builder/azdo/_work/9/s/xamarin-macios/src/ObjCRuntime/Runtime.cs:line 2742
   at UIKit.UIApplication.UIApplicationMain(Int32 argc, String[] argv, IntPtr principalClassName, IntPtr delegateClassName) in /Users/builder/azdo/_work/9/s/xamarin-macios/src/UIKit/UIApplication.cs:line 64
   at UIKit.UIApplication.Main(String[] args, Type principalClass, Type delegateClass) in /Users/builder/azdo/_work/9/s/xamarin-macios/src/UIKit/UIApplication.cs:line 96
   at MauiCollectionViewGroupChanging.iOS.Program.Main(String[] args) in D:\Projects\Test\MauiCollectionViewGroupChanging\MauiCollectionViewGroupChanging.iOS\Main.cs:line 13

=================================================================
	Native Crash Reporting
=================================================================
Got a SIGABRT while executing native code. This usually indicates
a fatal error in the mono runtime or one of the native libraries 
used by your application.
=================================================================

=================================================================
	Native stacktrace:
=================================================================
	0x105aef878 - /Users/michael/Library/Developer/CoreSimulator/Devices/70115689-3898-4F0F-93E5-861C8E4379A4/data/Containers/Bundle/Application/692A8BF7-FDD4-402F-8867-A53074C126D3/MauiCollectionViewGroupChanging.iOS.app/libmonosgen-2.0.dylib : mono_dump_native_crash_info
	0x105a9c63c - /Users/michael/Library/Developer/CoreSimulator/Devices/70115689-3898-4F0F-93E5-861C8E4379A4/data/Containers/Bundle/Application/692A8BF7-FDD4-402F-8867-A53074C126D3/MauiCollectionViewGroupChanging.iOS.app/libmonosgen-2.0.dylib : mono_handle_native_crash
	0x105c673c0 - /Users/michael/Library/Developer/CoreSimulator/Devices/70115689-3898-4F0F-93E5-861C8E4379A4/data/Containers/Bundle/Application/692A8BF7-FDD4-402F-8867-A53074C126D3/MauiCollectionViewGroupChanging.iOS.app/libmonosgen-2.0.dylib : sigabrt_signal_handler.cold.1
	0x105aef15c - /Users/michael/Library/Developer/CoreSimulator/Devices/70115689-3898-4F0F-93E5-861C8E4379A4/data/Containers/Bundle/Application/692A8BF7-FDD4-402F-8867-A53074C126D3/MauiCollectionViewGroupChanging.iOS.app/libmonosgen-2.0.dylib : mono_runtime_setup_stat_profiler
	0x1053d03c0 - /usr/lib/system/libsystem_platform.dylib : _sigtramp
	0x10541b408 - /usr/lib/system/libsystem_pthread.dylib : pthread_kill
	0x1801704ec - /Library/Developer/CoreSimulator/Volumes/iOS_22C150/Library/Developer/CoreSimulator/Profiles/Runtimes/iOS 18.2.simruntime/Contents/Resources/RuntimeRoot/usr/lib/system/libsystem_c.dylib : abort
	0x10577c984 - /Users/michael/Library/Developer/CoreSimulator/Devices/70115689-3898-4F0F-93E5-861C8E4379A4/data/Containers/Bundle/Application/692A8BF7-FDD4-402F-8867-A53074C126D3/MauiCollectionViewGroupChanging.iOS.app/libxamarin-dotnet-debug.dylib : xamarin_find_protocol_wrapper_type
	0x105b7fac8 - /Users/michael/Library/Developer/CoreSimulator/Devices/70115689-3898-4F0F-93E5-861C8E4379A4/data/Containers/Bundle/Application/692A8BF7-FDD4-402F-8867-A53074C126D3/MauiCollectionViewGroupChanging.iOS.app/libmonosgen-2.0.dylib : mono_invoke_unhandled_exception_hook
	0x105a5f9cc - /Users/michael/Library/Developer/CoreSimulator/Devices/70115689-3898-4F0F-93E5-861C8E4379A4/data/Containers/Bundle/Application/692A8BF7-FDD4-402F-8867-A53074C126D3/MauiCollectionViewGroupChanging.iOS.app/libmonosgen-2.0.dylib : mono_jit_exec
	0x105791bac - /Users/michael/Library/Developer/CoreSimulator/Devices/70115689-3898-4F0F-93E5-861C8E4379A4/data/Containers/Bundle/Application/692A8BF7-FDD4-402F-8867-A53074C126D3/MauiCollectionViewGroupChanging.iOS.app/libxamarin-dotnet-debug.dylib : xamarin_main
	0x1044ded50 - /Users/michael/Library/Developer/CoreSimulator/Devices/70115689-3898-4F0F-93E5-861C8E4379A4/data/Containers/Bundle/Application/692A8BF7-FDD4-402F-8867-A53074C126D3/MauiCollectionViewGroupChanging.iOS.app/MauiCollectionViewGroupChanging.iOS : main
	0x105115410 - Unknown
	0x105262274 - Unknown

=================================================================
	Basic Fault Address Reporting
=================================================================
Memory around native instruction pointer (0x1056bd108):0x1056bd0f8  c0 03 5f d6 c0 03 5f d6 10 29 80 d2 01 10 00 d4  .._..._..)......
0x1056bd108  e3 00 00 54 fd 7b bf a9 fd 03 00 91 ee e2 ff 97  ...T.{..........
0x1056bd118  bf 03 00 91 fd 7b c1 a8 c0 03 5f d6 c0 03 5f d6  .....{...._..._.
0x1056bd128  70 0a 80 d2 01 10 00 d4 e3 00 00 54 fd 7b bf a9  p..........T.{..

=================================================================
	Managed Stacktrace:
=================================================================
=================================================================

```
