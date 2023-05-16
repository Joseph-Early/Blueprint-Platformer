-- variables
col = 0
inc = true;
amount = 0.01;

-- tick function called 60 times a second
function Tick()
    -- randomly rotate platfom one and two around
    Rotate("plat_1", 1)
    Rotate("plat_2", -6)

    -- increase colour
	if inc then
        if col > 1 then
            inc = false
            goto after
        end
	    col = col + amount
    -- decrese colour
	else
        if col < 0 then
            inc = true
            goto after
        end
	    col = col - amount
	end

    -- change the colour for the player
    ::after::
	SetColour("player", col * 3, col * 2, col * 3, 1);

    -- loop through all the platforms and change the colour
    for i=1, 6 do
        SetColour("plat_" .. i, col * 1 / i, col * 2 / i, col * 5 / i, 1);
    end

	-- move between points
	MoveBetweenAbs("goal", 1, 1, -2, -2, col)


end

-- lerp function
function Lerp(a, b, t)
    return a + (b - a) * t
end

-- move between two points
function MoveBetweenAbs(object, x, y, x1, y1, time)
    MoveAbs(object, Lerp(x, x1, time), Lerp(y, y1, time))
end