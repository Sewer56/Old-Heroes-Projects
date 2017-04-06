WIP, will update this when complete.

Prerequisites:

To compile you must have:
Mono: http://www.mono-project.com/download/
GTK# for .NET: http://www.mono-project.com/download/

Optional: Monodevelop/Xamarin Studio

If you're on Linux, you're probably smart enough to compile this yourself, everything you'd need would likely be simply shipped with monodevelop on your distribution.

--------------

On some Linux distributions you may have to symlink a few GTK related libraries for the
program to work for you.

Try running:
sudo ln -s /usr/lib32/libglib-2.0.so /usr/lib32/libglib-2.0-0.so
sudo ln -s /usr/lib64/libglib-2.0.so /usr/lib64/libglib-2.0-0.so
sudo ln -s /usr/lib32/libgtk-win32-2.0.so /usr/lib32/libgtk-win32-2.0-0.so
sudo ln -s /usr/lib64/libgtk-win32-2.0.so /usr/lib64/libgtk-win32-2.0-0.so
