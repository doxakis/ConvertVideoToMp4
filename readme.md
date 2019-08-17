# ConvertVideoToMp4
A tool to convert any video to mp4. I create this tool to prepare videos in the right format for [LocalVideoStreaming](https://github.com/doxakis/LocalVideoStreaming)

All you have to do is to specify the folder and the thread count you allow the program to use.

It will search recursively for videos and skip videos which are already converted (same filename without the extension)

# Known issues
- It keeps the original video.
- If it is stopped while processing, it will not resume the conversion the next time and the mp4 file is broken. (should be removed and start the process again)

# Copyright and license
Code released under the MIT license.
