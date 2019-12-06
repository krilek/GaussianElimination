import matplotlib.pyplot as plt
import matplotlib.ticker as tic
import matplotlib.patches as mpatches
import numpy as np
from DataVisualization.results_reader import data_for


class Visualizer:
    def __init__(self):
        self.data_types = ['double', 'float', 'fraction']
        self.algorithms = {'G': 'Gauss', 'PG': 'Partial Gauss', 'FG': 'Full Gauss'}
        self.data_set = {key: data_for(key) for key in self.data_types}
        self.colors = ['m', 'c', 'g']
        self.colors_alg = {key: self.colors[list(self.algorithms).index(key)] for key in self.algorithms.keys()}
        self.colors_type = {key: self.colors[self.data_types.index(key)] for key in self.data_types}
        self.handles1 = [mpatches.Patch(color=self.colors_alg[algorithm], label=self.algorithms[algorithm]) for
                         algorithm
                         in
                         self.algorithms.keys()]
        self.handles2 = [mpatches.Patch(color=self.colors_type[variable], label=variable) for
                         variable in self.data_types]

    # time, g pg fg
    def graph(self, val_x: str, val_y: str, title: str):
        for data_type in self.data_types:
            fig, ax = plt.subplots()
            for algorithm in self.algorithms.keys():
                ax.plot(self.data_set[data_type][algorithm][val_x], self.data_set[data_type][algorithm][val_y],
                        self.colors_alg[algorithm])
            plt.grid()
            # plt.yscale('log')
            plt.legend(handles=self.handles1)
            plt.xlabel(f"{val_x}")
            plt.ylabel(f"{val_y}")
            plt.title(f"{data_type} - {title}")
            plt.show()

    def graph_2nd(self, val_x: str, val_y: str, title: str):
        for alg in self.algorithms.keys():
            fig, ax = plt.subplots()
            for var in self.data_types:
                ax.plot(self.data_set[var][alg][val_x], self.data_set[var][alg][val_y], self.colors_type[var])
            plt.grid()
            plt.legend(handles=self.handles2)
            plt.xlabel(f"{val_x}")
            plt.ylabel(f"{val_y}")
            plt.title(f"{self.algorithms[alg]} - {title}")
            plt.show()

    # czas, n = 500 , typ zmiennych, typ metody


def graph_e():
    data = [[15048, 15480, 55152],
            [908, 1008, 5],
            [21077762, 21482524, 35846720]]

    columns = ('Gauss', 'Partial Gauss', 'Full Gauss')
    rows = ['fraction', 'double', 'float']

    values = np.arange(0, 40000, 500)
    value_increment = 10000
    colors = plt.cm.BuPu(np.linspace(0.1, 0.5, len(data)))
    index = np.arange(len(columns))
    bar_width = 0.4
    y_offset = np.zeros(len(columns))
    cell_text = []
    for row in range(len(data)):
        plt.bar(index, data[row], bar_width, bottom=y_offset, color=colors[row])
        y_offset = y_offset + data[row]
        cell_text.append(['%1.3f s' % (x / 1000.0) for x in y_offset])
    colors = colors[::-1]
    cell_text.reverse()
    plt.table(cellText=cell_text,
              rowLabels=rows,
              rowColours=colors,
              colLabels=columns,
              loc='bottom')
    plt.subplots_adjust(left=0.2, bottom=0.2)
    plt.ylabel("time [ms]".format(value_increment))
    plt.yticks(values * value_increment, ['%d' % val for val in values])
    plt.xticks([])
    plt.title('running time for n=500')
    plt.yscale('log')
    plt.show()


if __name__ == "__main__":
    task3 = Visualizer()
    # task3.graph("n", "time", "running time")
    # task3.graph("n", "error", "accuracy")
    task3.graph_2nd("n", "time", "running time")
    # graph_e()
