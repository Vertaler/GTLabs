# -*- coding: utf8 -*-
import numpy as np

ACCURACY = 0.0001
AGENTS_COUNT = 8
MIN_OPINION = -1
MAX_OPINION = 1
U = -1
V = 1

float_formatter = lambda x: "%.2f" % x
np.set_printoptions(formatter={'float_kind':float_formatter})

def generate_influence_matrix(agents_count):
    A = np.random.rand(agents_count, agents_count)
    row_sums = np.sum(A, axis=1)

    for i in range(agents_count):
        for j in range(agents_count):
            A[i][j] /= row_sums[i]

    return A

def generate_opinion_vector(agents_count, min_opinion, max_opinion):
    x = np.random.rand(1, agents_count)[0]
    return (max_opinion-min_opinion)*x + min_opinion

def add_bought_agents(x, u, v):
    #Вектор с игроками, купившими агентов. 0 - означает независимого агента
    agents_player_vector = np.random.randint(0,3, len(x))

    first_player_agents = [i for i, elem in enumerate(agents_player_vector) if elem == 1]
    second_player_agents = [i for i, elem in enumerate(agents_player_vector) if elem == 2]

    print u"Игроки, купленные первым игроком: {}".format(first_player_agents)
    print u"Игроки, купленные вторым игроком: {}".format(second_player_agents)

    for i in first_player_agents:
        x[i] = u
    for i in second_player_agents:
        x[i] = v


def check_game_solved(A, x):
    for j in range(len(x)):
        for i in range(len(x)):
            if not np.absolute(A[i][j] - A[0][j]) < ACCURACY:
                return False
    for i in range(len(x)):
        if not np.absolute(x[i] - x[0]) < ACCURACY:
            return False
    return True

def solve_game(A, x_0):
    A_inf = np.matrix.copy(A)
    x = np.matrix.copy(x_0)
    while not check_game_solved(A_inf, x):
        A_inf = np.dot(A_inf, A_inf)
        x = np.dot(A, x)
    return A_inf, x

if __name__ == "__main__":
    A = generate_influence_matrix(AGENTS_COUNT)
    x_0 = generate_opinion_vector(AGENTS_COUNT, MIN_OPINION, MAX_OPINION)
    A_inf, x = solve_game(A, x_0)

    print u"Матрица влияния A :\n{}".format(A)
    print "-"*20
    print u"Решение игры с независимыми агентами:"
    print u"Начальный вектор мнений: \n{}".format(x_0)
    print u"Итоговая матрица влияние: \n{}\n".format(A_inf)
    print u"Итоговое мнение : \n{}\n".format(x)

    print "-" * 20
    print u'Решение игры с купленными игроками'
    add_bought_agents(x_0, U, V)
    A_inf, x = solve_game(A, x_0)
    print u"Начальный вектор мнений: \n{}".format(x_0)
    print u"Итоговая матрица влияния: \n{}".format(A_inf)
    print u"Итоговое мнение: \n{}\n".format(x)

