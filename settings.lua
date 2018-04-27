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
		["y"] = "й",
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
		["solid_sign"] = "ъ",
		["y"] = "ы",
		["soft_sign"] = "ь",
		["e"] = "э",
		["yu"] = "ю",
		["ya"] = "я",
		["_"] = " ",
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
	return "RusDactile_Graph.pb"
end

--Файл символов на выходе
function getOutputLabels ()
	return "RusDactile_Lables.txt"
end