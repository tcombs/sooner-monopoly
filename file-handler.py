import os

while True:
	if os.path.isfile('player_state1.txt'):
		# read in files
		fin = open('player_state1.txt', 'r+')
		pstr = fin.read()
		os.remove('player_state1.txt')
		fout = open('player_state.txt', 'r+')
		fout.write(pstr)

	if os.path.isfile('prop_state1.txt'):
		fin = open('prop_state1.txt', 'r+')
		pstr2 = fin.read()
		os.remove('prop_state1.txt')
		fout = open('prop_state.txt', 'r+')
		fout.write(pstr2)