#!/bin/sh

echo "$1"| cut -d'/' -f5 | sed -r 's/(^|-)([0-9a-z])/\U\2/g'
