import sys
import subprocess
import os
import os.path
import shutil

imageMagickPath = r'C:\Program Files\ImageMagick-6.8.9-Q16'
imageMagickConvert = imageMagickPath + r'\convert'

platform = sys.argv[1]

outFolder = 'out/' + platform

if os.path.exists(outFolder):
	shutil.rmtree(outFolder)
os.makedirs(outFolder)

def resizeImage(resource,w,h):
	directory = resource['d']
	fileName = resource['f']
	path = directory  + '/' + fileName
	newDirectory = outFolder + '/' + directory	
	if not os.path.exists(newDirectory):
		os.makedirs(newDirectory)
	newPath = newDirectory + '/' + fileName
	args = [imageMagickConvert, path, '-resize', '{}x{}'.format(w, h), newPath]
	print('Resizing ' + path + ' to: ' + str(w) + ',' + str(h))	
	subprocess.call(args)
	
resources = {}
categories = {}
rectangles = {}

f = open('resources.txt')
for line in f:
	tokens = str.split(line.strip(),',')
	
	rtype = tokens[0]
	enumname = tokens[1]	
	category = tokens[2]
	folder = tokens[3]
	fileName = tokens[4]
	
	resource = { 't': rtype, 'e': enumname, 'c': category, 'd': folder, 'f': fileName }
		
	if not rtype in resources:
		resources[rtype] = {}

	resources[rtype][resource['e']] = resource
		
	if category:
		if not category in categories:
			categories[category] = {}
		categories[category][resource['e']] = resource
f.close()

print()
print('Generating resource files...')

for key in resources.keys():
	print('Generating ' + key.lower() + ' resource file...')
	f = open(outFolder + '/' + key.lower()  + 'Resources.txt', 'w')
	fc = []
	for re in resources[key].values():
		fc.append(re['t'] + '_' + re['e'] + ',' + re['d'] + '/' + os.path.splitext(re['f'])[0])
	f.write('\n'.join(fc))
	f.close()

print()
print('Generating enum file...')

f = open(outFolder + '/ResourceEnum.cs', 'w')
fc = []
fc.append('namespace KS.PizzaEmpire.Unity')
fc.append('{')
fc.append('\tpublic enum ResourceEnum')
fc.append('\t{')
fc.append('\t\tNONE = 0,')
for key in resources.keys():
	for re in resources[key].values():
		fc.append('\t\t' + re['t'] + '_' + re['e'] + ',')
fc.append('\t}')
fc.append('}')
f.write('\n'.join(fc))
f.close()

f = open('resourceSizes-' + platform + '.txt')
for line in f:
	tokens = str.split(line.strip(),',')
	n = tokens[0]
	x = tokens[1]
	y = tokens[2]
	w = tokens[3]
	h = tokens[4]
	rectangles[n] = [ x, y, w, h ]
f.close()	

print()
print('Resizing images...')

for key in rectangles.keys():
	wrekt = rectangles[key]

	if key in categories:
		for re in categories[key].values():
			resizeImage(re,wrekt[2],wrekt[3])
	else:
		if key in resources['IMAGE']:
			resizeImage(resources['IMAGE'][key],w,h)
			
print()
print('Creating layout file...')

f = open(outFolder + '/rectangles.txt', 'w')
for key in rectangles.keys():
	wrekt = rectangles[key]

	if key in resources['IMAGE']:
		f.write(key + ',' + ','.join(wrekt))
		
f.close()

