cmake_minimum_required(VERSION 3.21)
project(HW5_Cpp_MemoryLeak_start)

set(CMAKE_CXX_STANDARD 14)

include_directories(.)

add_executable(HW5_Cpp_MemoryLeak_start
        Cpp-LinkedList-Sample.cpp
        Node.cpp
        Node.h)
