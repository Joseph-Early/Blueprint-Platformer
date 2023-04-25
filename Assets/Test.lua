col = 0
inc = true;
amount = 0.01;

function Tick()
    Rotate("P2", 1)
    Rotate("P3", -6)

	if inc then
        if col > 1 then
            inc = false
            goto after
        end
	    col = col + amount
	else
        if col < 0 then
            inc = true
            goto after
        end
	    col = col - amount
	end
    ::after::
	SetColour("P", col * 3, col * 2, col * 3, 1);
end