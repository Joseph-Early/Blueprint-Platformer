-- Exposed methods
Move("obj_name", x, y) -- Move relative
MoveAbs("obj_name", x, y) -- Move absolute
Rotate("obj_name", angle) -- Rotate relative
RotateAbs("obj_name", angle) -- Rotate absolute
Scale("obj_name", x, y) -- Scale relative
ScaleAbs("obj_name", x, y) -- Scale Absolute
SetColour("obj_name", r, g, b, a) -- Set Colour RGBA (0-1)
Print(text) -- Debug log for testing to Unity console
Destroy("obj_name") -- Destroy object

-- Code called at start


function Tick()
    -- Code called every frame (invoked 60 tinmes a second)

end

-- Lerp function
function Lerp(a, b, t)
    return a + (b - a) * t
end

-- Move between two points
function MoveBetweenAbs(object, x, y, x1, y1, time)
    MoveAbs(object, Lerp(x, x1, time), Lerp(y, y1, time))
end