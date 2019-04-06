using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;



namespace Model {
    static class GraphicsExtension {
        private static void DrawCubicCurve(this Graphics graphics, Pen pen, float beta, float step, PointF start, PointF end, float a3, float a2, float a1, float a0, float b3, float b2, float b1, float b0) {
            float xPrev, yPrev;
            float xNext, yNext;
            bool stop = false;

            xPrev = beta * a0 + (1 - beta) * start.X;
            yPrev = beta * b0 + (1 - beta) * start.Y;

            for (float t = step; ; t += step) {
                if (stop)
                    break;

                if (t >= 1) {
                    stop = true;
                    t = 1;
                }

                xNext = beta * (a3 * t * t * t + a2 * t * t + a1 * t + a0) + (1 - beta) * (start.X + (end.X - start.X) * t);
                yNext = beta * (b3 * t * t * t + b2 * t * t + b1 * t + b0) + (1 - beta) * (start.Y + (end.Y - start.Y) * t);

                graphics.DrawLine(pen, xPrev, yPrev, xNext, yNext);

                xPrev = xNext;
                yPrev = yNext;
            }
        }

        /// <summary>
        /// Draws a B-spline curve through a specified array of Point structures.
        /// </summary>
        /// <param name="pen">Pen for line drawing.</param>
        /// <param name="points">Array of control points that define the spline.</param>
        /// <param name="beta">Bundling strength, 0 <= beta <= 1.</param>
        /// <param name="step">Step of drawing curve, defines the quality of drawing, 0 < step <= 1</param>
        public static void DrawBSpline(this Graphics graphics, Pen pen, PointF[] points, float beta, float step) {
            if (points == null)
                throw new ArgumentNullException("The point array must not be null.");
            if (beta < 0 || beta > 1)
                throw new ArgumentException("The bundling strength must be >= 0 and <= 1.");
            if (step <= 0 || step > 1)
                throw new ArgumentException("The step must be > 0 and <= 1.");
            if (points.Length <= 1)
                return;
            if (points.Length == 2) {
                graphics.DrawLine(pen, points[0], points[1]);
                return;
            }

            float a3, a2, a1, a0, b3, b2, b1, b0;
            float deltaX = (points[points.Length - 1].X - points[0].X) / (points.Length - 1);
            float deltaY = (points[points.Length - 1].Y - points[0].Y) / (points.Length - 1);
            PointF start, end;

            {
                a0 = points[0].X;
                b0 = points[0].Y;

                a1 = points[1].X - points[0].X;
                b1 = points[1].Y - points[0].Y;

                a2 = 0;
                b2 = 0;

                a3 = (points[0].X - 2 * points[1].X + points[2].X) / 6;
                b3 = (points[0].Y - 2 * points[1].Y + points[2].Y) / 6;

                start = points[0];
                end = new PointF
                (
                  points[0].X + deltaX,
                  points[0].Y + deltaY
                );

                graphics.DrawCubicCurve(pen, beta, step, start, end, a3, a2, a1, a0, b3, b2, b1, b0);
            }

            for (int i = 1; i < points.Length - 2; i++) {
                a0 = (points[i - 1].X + 4 * points[i].X + points[i + 1].X) / 6;
                b0 = (points[i - 1].Y + 4 * points[i].Y + points[i + 1].Y) / 6;

                a1 = (points[i + 1].X - points[i - 1].X) / 2;
                b1 = (points[i + 1].Y - points[i - 1].Y) / 2;

                a2 = (points[i - 1].X - 2 * points[i].X + points[i + 1].X) / 2;
                b2 = (points[i - 1].Y - 2 * points[i].Y + points[i + 1].Y) / 2;

                a3 = (-points[i - 1].X + 3 * points[i].X - 3 * points[i + 1].X + points[i + 2].X) / 6;
                b3 = (-points[i - 1].Y + 3 * points[i].Y - 3 * points[i + 1].Y + points[i + 2].Y) / 6;

                start = new PointF
                (
                  points[0].X + deltaX * i,
                  points[0].Y + deltaY * i
                );

                end = new PointF
                (
                  points[0].X + deltaX * (i + 1),
                  points[0].Y + deltaY * (i + 1)
                );

                graphics.DrawCubicCurve(pen, beta, step, start, end, a3, a2, a1, a0, b3, b2, b1, b0);
            }

            {
                a0 = points[points.Length - 1].X;
                b0 = points[points.Length - 1].Y;

                a1 = points[points.Length - 2].X - points[points.Length - 1].X;
                b1 = points[points.Length - 2].Y - points[points.Length - 1].Y;

                a2 = 0;
                b2 = 0;

                a3 = (points[points.Length - 1].X - 2 * points[points.Length - 2].X + points[points.Length - 3].X) / 6;
                b3 = (points[points.Length - 1].Y - 2 * points[points.Length - 2].Y + points[points.Length - 3].Y) / 6;

                start = points[points.Length - 1];

                end = new PointF
                (
                  points[0].X + deltaX * (points.Length - 2),
                  points[0].Y + deltaY * (points.Length - 2)
                );

                graphics.DrawCubicCurve(pen, beta, step, start, end, a3, a2, a1, a0, b3, b2, b1, b0);
            }
        }
    }
}
