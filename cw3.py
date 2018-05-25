# -*- coding: utf8 -*-

from math import factorial as fac
from  itertools import product

ACCURACY = 0.0001

char_func = {
    0b0000:0,
    0b0001:2,
    0b0010:3,
    0b0100:4,
    0b1000:4,
    0b0011:6,
    0b0101:6,
    0b1001:7,
    0b0110:7,
    0b1010:8,
    0b1100:8,
    0b0111:11,
    0b1011:11,
    0b1101:12,
    0b1110:12,
    0b1111:14
}

def super_additive_check(char_func):
    U = len(char_func) - 1
    for i,j in product(range(len(char_func)), repeat=2):
        if i&j ==0 and char_func[i] + char_func[j] > char_func[i|j]:
            print "Не супераддитивная {:04b} {:04b} ".format(i, j)
            return False
    print 'Супераддтивная'
    return True

def convex_check(char_func):
    for i,j in product(range(len(char_func)), repeat=2):
        if  char_func[i] + char_func[j] > char_func[i|j] + char_func[i&j]:
            print "Не выпуклая {:04b} {:04b} ".format(i, j)
            return False
    print "Выпуклая"
    return True

def weigth(i):
    return bin(i).count('1')

def compute_shapley_vector(char_func):
    N = len(bin(len(char_func)-1).split('b')[1])
    N_fact = fac(N)
    U = len(char_func) - 1
    res = [0]*N
    for i in range(N):
        res[i] =sum([fac(weigth(s|1<<i)-1)*fac(N-weigth( (1<<i)|s ))*(char_func[s|(1<<i)] - char_func[~(1<<i)&s]) for s in range(len(char_func)) ])
    return map(lambda x: float(x)/(2*N_fact) ,res)

def group_rationalisation_check(shapley_vector, char_func):
    return (sum(shapley_vector) - float(char_func[len(char_func) - 1])) < ACCURACY

def individual_rationalisation_check(shapley_vector, char_func):
    N = len(bin(len(char_func)-1).split('b')[1])
    for i in range(N):
        if shapley_vector[i] < char_func[1<<i]:
            print "Нарушение индивидуальной рациональзации для игрока {}".format(i+1)
            return False
    return True

super_additive_check(char_func)
convex_check(char_func)
shapley_vector = compute_shapley_vector(char_func)
print 'Вектор Шепли {}'.format(shapley_vector)
if group_rationalisation_check(shapley_vector, char_func):
    print 'Групповая рационализация выполняется'
if individual_rationalisation_check(shapley_vector, char_func):
    print 'Индивидуальная рационализация выполняется'