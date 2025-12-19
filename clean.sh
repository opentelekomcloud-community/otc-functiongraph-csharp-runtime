#!/bin/bash

find ./libraries -iname "bin" | xargs rm -rf
find ./libraries -iname "obj" | xargs rm -rf
find ./samples-doc -iname "bin" | xargs rm -rf
find ./samples-doc -iname "obj" | xargs rm -rf
find ./samples-doc -iname "*.zip" | xargs rm -rf