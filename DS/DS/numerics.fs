module DS.numerics

#r "nuget: MathNet.Numerics.FSharp"
open MathNet.Numerics.LinearAlgebra

let A: Matrix<float> = 
  matrix [ [ 5.0; 2.0 ]; 
           [ 2.0; 3.0 ] ]
           
let b: Vector<float> = 
  vector [ 6.0; 2.0 ]

let x = A.Solve(b) |> printf "%A"
