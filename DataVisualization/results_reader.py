from math import sqrt


# https://stackoverflow.com/questions/3277503/how-to-read-a-file-line-by-line-into-a-list
# solution contributed by beco
def read_from_csv(file_name: str, column_names: list):
    with open(f"{file_name}.csv") as f:
        lines = [line.rstrip('\n') for line in f]
    container = {key: [] for key in column_names}
    for line in lines:
        i = 0
        for name in column_names:
            container[name].append(line.split(';', i + 1)[i])
            i += 1
    return container


def prepare_data():
    types = {'double': 'double_1573485363', 'float': 'float_1573485437',
             'fraction': 'fraction_1573505576'}
    data = {key: {} for key in types.keys()}
    columns = ['n', 'time', 'error', 'type']
    for type_name in types.keys():
        data[type_name] = read_from_csv(types[type_name], columns)
    return data


# time, g pg fg
def data_for(data_type: str):
    data_dict = prepare_data()[data_type]
    columns = ['n', 'time', 'error']
    algo_names = ['G', 'PG', 'FG']
    algo = {name: {key: [] for key in columns} for name in algo_names}
    indexes = {key: [] for key in algo_names}
    for name in algo_names:
        indexes[name] = [i for i, e in enumerate(data_dict['type']) if e == name]
    for name in algo_names:
        for i in indexes[name]:
            algo[name]['n'].append(int(data_dict['n'][i]))
            algo[name]['time'].append(int(data_dict['time'][i]))
            algo[name]['error'].append(sqrt(float(data_dict['error'][i].replace(',', '.'))))
    return algo
