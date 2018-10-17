namespace Fable.Import.Rough

open Fable.Core
open Fable.Import.Browser

[<AutoOpen>]
module Props =

    [<AutoOpen>]
    module Geometry =
        type Point =
            float * float

        and Line =
            Point * Point

        and [<AllowNullLiteral>] Rectangle =
            abstract x: float with get, set
            abstract y: float with get, set
            abstract width: float with get, set
            abstract height: float with get, set

        and [<AllowNullLiteral>] Segment =
            abstract px1: float with get, set
            abstract px2: float with get, set
            abstract py1: float with get, set
            abstract py2: float with get, set
            abstract xi: float with get, set
            abstract yi: float with get, set
            abstract a: float with get, set
            abstract b: float with get, set
            abstract c: float with get, set
            abstract _undefined: bool with get, set
            abstract isUndefined: unit -> bool
            abstract intersects: otherSegment: Segment -> bool

    [<AutoOpen>]
    module Core =
        type Config =
            | Async of bool
            | Options of Options
            | NoWorker of bool
            | WorklyURL of string

        and [<AllowNullLiteral>] DrawingSurface =
            abstract width: U2<float, SVGAnimatedLength> with get, set
            abstract height: U2<float, SVGAnimatedLength> with get, set


        and ResolvedOptions =
            | MaxRandomnessOffset of float
            | Roughness of float
            | Bowing of float
            | Stroke of string
            | StrokeWidth of float
            | CurveTightness of float
            | CurveStepCount of float
            | Fill of string
            | FillStyle of string
            | FillWeight of float
            | HachureAngle of float
            | HachureGap of float
            | Simplification of float

        and Options =
            | MaxRandomnessOffset of float
            | Roughness of float
            | Bowing of float
            | Stroke of string
            | StrokeWidth of float
            | CurveTightness of float
            | CurveStepCount of float
            | Fill of string
            | FillStyle of string
            | FillWeight of float
            | HachureAngle of float
            | HachureGap of float
            | Simplification of float


        and [<StringEnum; RequireQualifiedAccess>] OpType =
            | Move
            | BcurveTo
            | LineTo
            | QcurveTo

        and [<StringEnum>] [<RequireQualifiedAccess>] OpSetType =
            | Path
            | FillPath
            | FillSketch
            | Path2Dfill
            | Path2Dpattern

        and [<AllowNullLiteral>] Op =
            abstract op: OpType with get, set
            abstract data: ResizeArray<float> with get, set

        and [<AllowNullLiteral>] OpSet =
            abstract ``type``: OpSetType with get, set
            abstract ops: ResizeArray<Op> with get, set
            abstract size: Point option with get, set
            abstract path: string option with get, set

        and [<AllowNullLiteral>] Drawable =
            abstract shape: string with get, set
            abstract options: ResolvedOptions with get, set
            abstract sets: ResizeArray<OpSet> with get, set

        and [<AllowNullLiteral>] PathInfo =
            abstract d: string with get, set
            abstract stroke: string with get, set
            abstract strokeWidth: float with get, set
            abstract fill: string option with get, set
            abstract pattern: PatternInfo option with get, set

        and [<AllowNullLiteral>] PatternInfo =
            abstract x: float with get, set
            abstract y: float with get, set
            abstract width: float with get, set
            abstract height: float with get, set
            abstract viewBox: string with get, set
            abstract patternUnits: string with get, set
            abstract path: PathInfo with get, set

    [<AutoOpen>]
    module Renderer =
        type [<AllowNullLiteral>] RoughRenderer =
            abstract line: x1: float * y1: float * x2: float * y2: float * o: ResolvedOptions -> OpSet
            abstract linearPath: points: ResizeArray<Point> * close: bool * o: ResolvedOptions -> OpSet
            abstract polygon: points: ResizeArray<Point> * o: ResolvedOptions -> OpSet
            abstract rectangle: x: float * y: float * width: float * height: float * o: ResolvedOptions -> OpSet
            abstract curve: points: ResizeArray<Point> * o: ResolvedOptions -> OpSet
            abstract ellipse: x: float * y: float * width: float * height: float * o: ResolvedOptions -> OpSet
            abstract arc: x: float * y: float * width: float * height: float * start: float * stop: float * closed: bool * roughClosure: bool * o: ResolvedOptions -> OpSet
            abstract svgPath: path: string * o: ResolvedOptions -> OpSet
            abstract solidFillPolygon: points: ResizeArray<Point> * o: ResolvedOptions -> OpSet
            abstract patternFillPolygon: points: ResizeArray<Point> * o: ResolvedOptions -> OpSet
            abstract patternFillEllipse: cx: float * cy: float * width: float * height: float * o: ResolvedOptions -> OpSet
            abstract patternFillArc: x: float * y: float * width: float * height: float * start: float * stop: float * o: ResolvedOptions -> OpSet
            abstract getOffset: min: float * max: float * ops: ResolvedOptions -> float
            abstract doubleLine: x1: float * y1: float * x2: float * y2: float * o: ResolvedOptions -> ResizeArray<Op>
            abstract _line: obj with get, set
            abstract _curve: obj with get, set
            abstract _ellipse: obj with get, set
            abstract _curveWithOffset: obj with get, set
            abstract _arc: obj with get, set
            abstract _bezierTo: obj with get, set
            abstract _processSegment: obj with get, set

    [<AutoOpen>]
    module Generator =
        type [<AllowNullLiteral>] RoughGeneratorBase =
            abstract config: Config with get, set
            abstract surface: DrawingSurface with get, set
            abstract renderer: RoughRenderer with get, set
            abstract defaultOptions: ResolvedOptions with get, set
            abstract _options: ?options: Options -> ResolvedOptions
            abstract _drawable: shape: string * sets: ResizeArray<OpSet> * options: ResolvedOptions -> Drawable
            abstract lib: RoughRenderer
            abstract getCanvasSize: obj with get, set
            abstract computePolygonSize: points: ResizeArray<Point> -> Point
            abstract polygonPath: points: ResizeArray<Point> -> string
            abstract computePathSize: d: string -> Point
            abstract toPaths: drawable: Drawable -> ResizeArray<PathInfo>
            abstract fillSketch: obj with get, set
            abstract opsToPath: drawing: OpSet -> string

        and [<AllowNullLiteral>] RoughGenerator =
            inherit RoughGeneratorBase
            abstract line: x1: float * y1: float * x2: float * y2: float * ?options: Options -> 'a
            abstract rectangle: x: float * y: float * width: float * height: float * ?options: Options -> 'a
            abstract ellipse: x: float * y: float * width: float * height: float * ?options: Options -> 'a
            abstract circle: x: float * y: float * diameter: float * ?options: Options -> 'a
            abstract linearPath: points: ResizeArray<Point> * ?options: Options -> 'a
            abstract arc: x: float * y: float * width: float * height: float * start: float * stop: float * ?closed: bool * ?options: Options -> 'a
            abstract curve: points: ResizeArray<Point> * ?options: Options -> 'a
            abstract polygon: points: ResizeArray<Point> * ?options: Options -> 'a
            abstract path: d: string * ?options: Options -> 'a


    [<AutoOpen>]
    module Canvas =
        type [<AllowNullLiteral>] RoughCanvasBase =
            abstract canvas: HTMLCanvasElement with get, set
            abstract ctx: CanvasRenderingContext2D with get, set
            abstract getDefaultOptions: unit -> ResolvedOptions
            abstract draw: drawable: Drawable -> unit
            abstract computeBBox: obj with get, set
            abstract fillSketch: obj with get, set
            abstract _drawToContext: obj with get, set

        and [<AllowNullLiteral>] RoughCanvas =
            inherit RoughCanvasBase
            abstract gen: obj with get, set
            abstract generator: RoughGenerator
            abstract getDefaultOptions: unit -> ResolvedOptions
            abstract line: x1: float * y1: float * x2: float * y2: float * ?options: obj -> 'a
            abstract rectangle : x: float * y: float * width: float * height: float * ?options: obj -> 'a
            abstract ellipse: x: float * y: float * width: float * height: float * ?options: obj -> 'a
            abstract circle: x: float * y: float * diameter: float * ?options: obj -> 'a
            abstract linearPath: points: ResizeArray<Point> * ?options: obj -> 'a
            abstract polygon: points: ResizeArray<Point> * ?options: obj -> 'a
            abstract arc: x: float * y: float * width: float * height: float * start: float * stop: float * ?closed: bool * ?options: obj -> 'a
            abstract curve: points: ResizeArray<Point> * ?options: obj -> 'a
            abstract path: d: string * ?options: obj -> 'a


    [<AutoOpen>]
    module Svg =
        type [<AllowNullLiteral>] RoughSVGBase =
            abstract svg: SVGSVGElement with get, set
            abstract _defs: SVGDefsElement option with get, set
            abstract getDefaultOptions: unit -> ResolvedOptions
            abstract opsToPath: drawing: OpSet -> string
            abstract defs: SVGDefsElement option
            abstract draw: drawable: Drawable -> SVGGElement
            abstract fillSketch: obj with get, set

        and [<AllowNullLiteral>] RoughSVG =
            inherit RoughSVGBase
            abstract gen: obj with get, set
            abstract generator: RoughGenerator
            abstract getDefaultOptions: unit -> ResolvedOptions
            abstract opsToPath: drawing: OpSet -> string
            abstract line: x1: float * y1: float * x2: float * y2: float * ?options: obj -> 'a
            abstract rectangle: x: float * y: float * width: float * height: float * ?options: obj -> 'a
            abstract ellipse: x: float * y: float * width: float * height: float * ?options: obj -> 'a
            abstract circle: x: float * y: float * diameter: float * ?options: obj -> 'a
            abstract linearPath: points: ResizeArray<Point> * ?options: obj -> 'a
            abstract polygon: points: ResizeArray<Point> * ?options: obj -> 'a
            abstract arc: x: float * y: float * width: float * height: float * start: float * stop: float * ?closed: bool * ?options: obj -> 'a
            abstract curve: points: ResizeArray<Point> * ?options: obj -> 'a
            abstract path: d: string * ?options: obj -> 'a


    type Rough =
        abstract canvas : HTMLCanvasElement -> obj -> Canvas.RoughCanvas
        abstract svg : SVGSVGElement -> obj -> Svg.RoughSVG
        abstract createRenderer : unit -> Renderer.RoughRenderer
        abstract generator : obj -> DrawingSurface -> Generator.RoughGenerator

