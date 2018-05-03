--Tensorflow на Windows платформе не дружит с кирилицей. Будем иметь про запас ассоциативный массив всех возможных дактилей.
function getTranslatedFolders ()
	local table = { 
		["a"] = "а",
		["b"] = "б",
		["v"] = "в",
		["g"] = "г",
		["d"] = "д",
		["e"] = "е",
		["yo"] = "ё",
		["zh"] = "ж",
		["z"] = "з",
		["i"] = "и",
		["yi"] = "й",
		["k"] = "к",
		["l"] = "л",
		["m"] = "м",
		["n"] = "н",
		["o"] = "о",
		["p"] = "п",
		["r"] = "р",
		["s"] = "с",
		["t"] = "т",
		["u"] = "у",
		["f"] = "ф",
		["h"] = "х",
		["ts"] = "ц",
		["ch"] = "ч",
		["sh"] = "ш",
		["sch"] = "щ",
		["solidsign"] = "ъ",
		["y"] = "ы",
		["softsign"] = "ь",
		["ie"] = "э",
		["yu"] = "ю",
		["ya"] = "я",
		["space"] = " ",
		["comma"] = ",",
		["dot"] = "."
	}

	return table
end

--Папка с папками изображений
function getTrainFolderName ()
	return "TrainImages"
end

--Название графа на выходе
function getOutputGraph ()
	return "rusdactile_graph.pb"
end

--Файл символов на выходе
function getOutputLabels ()
	return "rusdactile_lables.txt"
end

--Укказываем порог для выделения жеста в метрах.
function getGestureDepthThresold ()
    return 0.04;
end