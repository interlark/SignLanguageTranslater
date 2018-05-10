# Переводчик глухонемого языка

**SignLanguageTranslater** написан для помощи в переводе с глухонемного русского языка жестов в обычный русский язык. Построен на глубоком машинном обучении с помощью библиотеки Google's **Tensorflow** и захвата изображения **Kinect v2 для Windows**.


# Описание
Программа состоит из двух модулей.
- Первый - **Тренировочный модуль**, тут использованы технологии **Tensorflow** для тренировки жестов сделанных особым образом снятые через **Kinect v2**.
Необходимо до использования установить **Python >= 3.6** и установить пакет **tensorflow-1.4.0** или  **tensorflow-1.4.0-gpu** для уменьшения времени работы скрипта обучения модели. (Только для владельцев Geforce NVIDIA).
![Gesture Manager](https://github.com/interlark/SignLanguageTranslater/raw/master/screenshots/gesture_manager.jpg)
![Capturing Gesture](https://github.com/interlark/SignLanguageTranslater/raw/master/screenshots/capturing_gesture.jpg)

![Model Training](https://github.com/interlark/SignLanguageTranslater/raw/master/screenshots/python_training.jpg)
- Второй - непосредственно **Модуль перевода**.
Параметры для управления: Пропуск кадров для выравнивания fps и Размер очереди для увеличения точности нестатичных жестов. Следует учитывать корреляцию этих параметров при запуске модуля. Так же вам необходимо указать полученные **тренировочным модулем** ***модель*** и ***символы модели***

![Gesture Recognition Main](https://github.com/interlark/SignLanguageTranslater/raw/master/screenshots/gesture_recognition_main.jpg)


![Gesture Recognition Н](https://github.com/interlark/SignLanguageTranslater/raw/master/screenshots/gesture_recognition_n.jpg)
![Gesture Recognition У](https://github.com/interlark/SignLanguageTranslater/raw/master/screenshots/gesture_recognition_u.jpg)

![Gesture Recognition У](https://github.com/interlark/SignLanguageTranslater/raw/master/screenshots/gesture_recognition_comma.jpg)

> Для контрибуторов:
Узел входа модели при использовании архитектуры **mobilenet** - ***input***, у **inception v3** - ***Mul***. Выхода - ***final_result***. Информация по пакетам работающим с моделями: **EmguTF** - наблюдаются проблемы с памятью при загрузке изображения и преобразования его в тензор. **TensorflowSharp** имеет по стандарту библиотеку ***libtensorflow***, работает с CPU при работе с моделью, компилировал с gpu поддержкой - наблюдаются лютые утечки памяти в циклах.
В релизе оставил ***libtensorflow.dll*** c CPU, проблем с утечкой не наблюдается. Скорость работы - от 2 fps до 10 fps c архитектурой ***mobilenet_1.0_224*** (**Kinect v2**'s ColorCamera переключается от 15 до 30 fps самостоятельно в зависимости от освещения). При бОльзших захваченных исходных данных - увеличивается точность и скорость модели, может достигать 30 fps.

## P.S.

Все динамические настройки - параметры выведены в корень проекта и описаны в виде функций на языке ***lua***.
Файл **Settings.luа**, в нем описан словарь символов, порог чувствительности, порог в % для принятия решения, что жест распознан, и.т.д.

## TODO

В следующиз реализах надо добавить:

- В **Модуле переводчике** выделять жест динамично, основываясь на **Kinect** индексах и выборе правой или левой точки - запястей или ладоней. Сейчас окно забора жеста статично.
- Как вы заметили, реализована только дактильная азбука глухонемых, с помощью **Kinect** добавить более сложные фонемы - джестуно.
- В **Тренировочном модуле** добавить возможность строить модель по инфракрасной камере. Для условий работы в темное время суток или других условий малого доступа света.
- И.т.д. 

## Лицензия

### [GNU General Public License V3](https://en.wikipedia.org/wiki/GNU_General_Public_License)
