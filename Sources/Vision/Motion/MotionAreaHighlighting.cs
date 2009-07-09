﻿namespace AForge.Vision.Motion
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    using AForge.Imaging;

    public class MotionAreaHighlighting : IMotionProcessing
    {
        private Color highlightColor = Color.Red;

        public unsafe void ProcessFrame( UnmanagedImage videoFrame, UnmanagedImage motionFrame )
        {
            int width  = videoFrame.Width;
            int height = videoFrame.Height;

            if ( ( motionFrame.Width != width ) || ( motionFrame.Height != height ) )
                return;

            byte* src = (byte*) videoFrame.ImageData.ToPointer( );
            byte* motion = (byte*) motionFrame.ImageData.ToPointer( );

            int srcOffset = videoFrame.Stride - width * 3;
            int motionOffset = motionFrame.Stride - width;

            byte fillR = highlightColor.R;
            byte fillG = highlightColor.G;
            byte fillB = highlightColor.B;

            for ( int y = 0; y < height; y++ )
            {
                for ( int x = 0; x < width; x++, motion++, src += 3 )
                {
                    if ( ( *motion != 0 ) && ( ( ( x + y ) & 1 ) == 0 ) )
                    {
                        src[RGB.R] = fillR;
                        src[RGB.G] = fillG;
                        src[RGB.B] = fillB;
                    }
                }
                src += srcOffset;
                motion += motionOffset;
            }
        }

        public void Reset( )
        {
        }
    }
}