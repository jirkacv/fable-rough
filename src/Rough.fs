namespace Fable.Import.Rough

open Fable.Core
open Fable.Import.Browser
open Fable.Import
open Fable.Core.JsInterop

module Helpers =
    let Roughjs: Rough = importDefault "roughjs"

    let keyValueListLowerFirst list =
        keyValueList CaseRules.LowerFirst list
    

    [<AbstractClass>]
    type RoughCanvasBase<'a> (canvas: HTMLCanvasElement, config: Config list) =
        member this.Canvas: Canvas.RoughCanvas = Roughjs.canvas canvas (config |> keyValueListLowerFirst)

        member this.Line (x1: float, y1: float, x2: float, y2: float, options: Options list): 'a =
            this.Canvas.line (x1, y1, x2, y2, options |> keyValueListLowerFirst)

        member this.Rectangle (x: float, y: float, width: float, height: float, options: Options list): 'a =
            this.Canvas.rectangle (x, y, width, height, options |> keyValueListLowerFirst)

        member this.Ellipse (x: float, y: float, width: float, height: float, options: Options list): 'a =
            this.Canvas.ellipse (x, y, width, height, options |> keyValueListLowerFirst)

        member this.Circle (x: float, y: float, diameter: float, options: Options list): 'a =
            this.Canvas.circle (x, y, diameter, options |> keyValueListLowerFirst)

        member this.LinearPath (points: ResizeArray<Point>, options: Options list): 'a =
            this.Canvas.linearPath (points, options |> keyValueListLowerFirst)

        member this.Polygon (points: ResizeArray<Point>, options: Options list): 'a =
            this.Canvas.polygon (points, options |> keyValueListLowerFirst)

        member this.Arc (x: float, y: float, width: float, height: float, start: float, stop: float, closed: bool, options: Options list): 'a =
            this.Canvas.arc (x, y, width, height, start, stop, closed, options |> keyValueListLowerFirst)

        member this.Curve (points: ResizeArray<Point>, options: Options list): 'a =
            this.Canvas.curve (points, options |> keyValueListLowerFirst)

        member this.Path (d: string, options: Options list): 'a =
            this.Canvas.path (d, options |> keyValueListLowerFirst)


    [<AbstractClass>]
    type RoughSvgBase<'a> (svg: SVGSVGElement, config: Config list) =
        member this.Svg =
            Roughjs.svg svg (config |> keyValueListLowerFirst)

        member this.Line (x1: float, y1: float, x2: float, y2: float, options: Options list): 'a =
            this.Svg.line (x1, y1, x2, y2, options |> keyValueListLowerFirst)

        member this.Rectangle (x: float, y: float, width: float, height: float, options: Options list): 'a =
            this.Svg.rectangle (x, y, width, height, options |> keyValueListLowerFirst)

        member this.Ellipse (x: float, y: float, width: float, height: float, options: Options list): 'a =
            this.Svg.ellipse (x, y, width, height, options |> keyValueListLowerFirst)

        member this.Circle (x: float, y: float, diameter: float, options: Options list): 'a =
            this.Svg.circle (x, y, diameter, options |> keyValueListLowerFirst)

        member this.LinearPath (points: ResizeArray<Point>, options: Options list): 'a =
            this.Svg.linearPath (points, options |> keyValueListLowerFirst)

        member this.Polygon (points: ResizeArray<Point>, options: Options list): 'a =
            this.Svg.polygon (points, options |> keyValueListLowerFirst)

        member this.Arc (x: float, y: float, width: float, height: float, start: float, stop: float, closed: bool, options: Options list): 'a =
            this.Svg.arc (x, y, width, height, start, stop, closed, options |> keyValueListLowerFirst)

        member this.Curve (points: ResizeArray<Point>, options: Options list): 'a =
            this.Svg.curve (points, options |> keyValueListLowerFirst)

        member this.Path (d: string, options: Options list): 'a =
            this.Svg.path (d, options |> keyValueListLowerFirst)



    let removeAsyncConfig (config: Config list) = 
        List.except [Async true; Async false] config
        
    let addAsyncConfig (config: Config list) =
        List.append (removeAsyncConfig config) [(Async true)]



    type RoughCanvas(canvas: HTMLCanvasElement, config: Config list) = 
        inherit RoughCanvasBase<Drawable>(canvas, config |> removeAsyncConfig)


    type RoughCanvasAsync(canvas: HTMLCanvasElement, config: Config list) =
        inherit RoughCanvasBase<JS.Promise<Drawable>>(canvas, config |> addAsyncConfig)


    type RoughSvg(svg: SVGSVGElement, config: Config list) = 
        inherit RoughSvgBase<SVGGElement>(svg, config |> removeAsyncConfig)


    type RoughSvgAsync(svg: SVGSVGElement, config: Config list) =
        inherit RoughSvgBase<JS.Promise<SVGGElement>>(svg, config |> addAsyncConfig)        