# coding=utf-8
import math
import random

def format_list(lst, precision):
    format_string = ("{:." + str(precision) + "f} ")*len(lst)
    format_string = "( " + format_string +")"
    return format_string.format(*lst)


def solve_analytical(l):
    n = int(math.ceil(1.0 / (2*l)))
    coef = float(1-2*l)/(n-1)
    first_player_points = [l + coef*i for i in range(n)]
    second_player_points = [float(i)/(n-1) for i in range(n)]
    game_price = 1.0 / n
    return first_player_points, second_player_points, game_price

def solve_numerical(l, iterations_count):
    first_player_wins = 0
    for i in range(iterations_count):
        x = random.uniform(0, 1)
        y = random.uniform(0, 1)
        if math.fabs(x-y) <= l:
            first_player_wins += 1
    return float(first_player_wins)/iterations_count

if __name__ == "__main__":
    l = 0.07
    iterations_count = 1000
    print u"Игра поиска на отрезке для l={:.3f}".format(l)
    first_player_points, second_player_points, game_price_analytical = solve_analytical(l)
    print u"\nРезультат аналитического решения: "
    print u"Первый игрок должен равновероятно выбирать одну из точек: " + format_list(first_player_points, 3)
    print u"Второй игрок должен равновероятно выбирать одну из точек: " + format_list(second_player_points, 3)
    print u"Цена игры: {:.3f}".format(game_price_analytical)

    game_price_numerical = solve_numerical(l, iterations_count)
    print u"\nРезультат численного решения, для {} итераций:".format(iterations_count)
    print u"Цена игры: {:.3f}".format(game_price_numerical)

    delta_price = math.fabs(float(game_price_numerical - game_price_analytical))/game_price_numerical
    print u"\nОтносительная погрешность численного решения: {:.3f}".format(delta_price)

