import os
import cv2
import numpy as np

def bigavif(p):
	if not os.path.isfile('0/af/'+p):
		return False
	if(os.path.getsize('0/af/'+p) - os.path.getsize('0/pq/'+p))< 100:
		return True
	return False

fyo=os.listdir('alph')
fyo.sort()
fyo=fyo[:-1]

for alphna in fyo:
	zet = alphna.split('.')
	if len(zet) !=3:
		break
	al=cv2.imread('alph/'+alphna, cv2.IMREAD_GRAYSCALE).astype(np.uint)
	al=np.right_shift(al*int(zet[1],16),12).astype(np.uint8)
	cv2.imwrite('alph/'+zet[0]+'.png', al)
	os.remove('alph/'+alphna)

fyo=os.listdir('0')
fyo.sort()
fyo=fyo[:-2]

emptyz=np.zeros(4, dtype=np.uint8)

for p in fyo:
	if os.path.isfile('0/af/'+p) or p.endswith('.png.png'):
		continue
	ymg=cv2.imread('0/'+p, cv2.IMREAD_UNCHANGED)
	h, w, chan = ymg.shape
	altalph='alph/'+p
	if os.path.isfile(altalph):
		amap=cv2.imread(altalph, cv2.IMREAD_UNCHANGED)
		for y in range(h):
			for x in range(w):
				if amap[y][x] < 4:
					ymg[y][x]=emptyz
		cv2.imwrite('tmpklean.png', ymg)
		os.system('zavif2.bat '+p)
	else:
		for y in range(h):
			for x in range(w):
				if ymg[y][x][3] < 4:
					ymg[y][x]=emptyz

		cv2.imwrite('tmpklean.png', ymg)
		os.system('zavif1.bat '+p)
		if bigavif(p):
			os.remove('0/pq/'+p)