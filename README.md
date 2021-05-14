# ChuuuVR for Koikatsu Party VR

This is a bare-bones plugin for Koikatsu Party VR that introduces kissing. It works basically like a toned down version of CyuVR, without the DLC requirement and the compatibility issues mentioned in [MayouKurayami's repo](https://github.com/MayouKurayami/KK_CyuVR). No code has been copied from CyuVR.

Has some semi-rare random glitches, but I think it's usable enough.

## Installation
Prerequisites:
* Koikatsu Party VR DLC
* HF Patch (probably works with just [KKAPI](https://github.com/ManlyMarco/KKAPI); haven't tried)

Download the DLL from the latest release and move it into `<your game directory>\BepInEx\plugins`.

## Building from source
Prerequisites:
* Visual Studio
* Koikatsu Party VR DLC

Steps:
1. Clone and open the solution in Visual Studio
1. Build it
1. It will fail
1. Replace `packages\IllusionLibs.Koikatu.Assembly-CSharp.2019.4.27\lib\net35\Assembly-CSharp.dll` with `<your game directory>\Koikatsu Party VR_Data\Managed\Assembly-CSharp.dll`
1. Build the project again
1. It should pass this time

Rebuilding won't overwrite the DLL you replaced, so you only need to do it once.

## Usage
The heroine should automatically start kissing when your head is close enough to hers, assuming she has a reasonably sized head and no other mods are interfering with the voice or the VR controller logic.

In caress mode, the kissing animation/sound will loop until you move your head away. In all other modes, the kissing effect lasts around 8-10 seconds, and you can start it again by moving your head away and then back. (Will probably fix this.)
