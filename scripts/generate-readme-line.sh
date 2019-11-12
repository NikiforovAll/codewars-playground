#!/bin/sh

EXERCISE=$(echo "$1" | cut -d'/' -f5)
EXERCISE_CC=$(echo $EXERCISE | sed -r 's/(^|-)([0-9a-z])/\U\2/g')
KYU=$2
echo "$KYU | [$EXERCISE_CC](https://www.codewars.com/kata/$EXERCISE/) | [$EXERCISE_CC.cs](https://github.com/NikiforovAll/codewars-playground/blob/master/src/$KYU/$EXERCISE_CC.cs)"

