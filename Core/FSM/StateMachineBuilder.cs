﻿/*
Developer       : Real Serious Games
First release   : 11/01/2018
File            : FSM/State.cs
Revision        : 1.0.1
Changelog       :   
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.Core.FSM
{
    /// <summary>
    /// Entry point for fluent API for constructing states. <para></para>
    /// <see cref="https://github.com/LeonardoADS08/ForgeSDK/wiki/FSM"/>
    /// </summary>
    public class StateMachineBuilder
    {
        /// <summary>
        /// Root level state.
        /// </summary>
        private readonly State rootState;

        /// <summary>
        /// Entry point for constructing new state machines.
        /// </summary>
        public StateMachineBuilder()
        {
            rootState = new State();
        }

        /// <summary>
        /// Create a new state of a specified type and add it as a child of the root state.
        /// </summary>
        /// <typeparam name="T">Type of the state to add</typeparam>
        /// <returns>Builder used to configure the new state</returns>
        public IStateBuilder<T, StateMachineBuilder> State<T>() where T : AbstractState, new()
        {
            return new StateBuilder<T, StateMachineBuilder>(this, rootState);
        }

        /// <summary>
        /// Create a new state of a specified type with a specified name and add it as a
        /// child of the root state.
        /// </summary>
        /// <typeparam name="T">Type of the state to add</typeparam>
        /// <param name="stateName">Name for the new state</param>
        /// <returns>Builder used to configure the new state</returns>
        public IStateBuilder<T, StateMachineBuilder> State<T>(string stateName) where T : AbstractState, new()
        {
            return new StateBuilder<T, StateMachineBuilder>(this, rootState, stateName);
        }

        /// <summary>
        /// Create a new state with a specified name and add it as a
        /// child of the root state.
        /// </summary>
        /// <param name="stateName">Name for the new state</param>
        /// <returns>Builder used to configure the new state</returns>
        public IStateBuilder<State, StateMachineBuilder> State(string stateName)
        {
            return new StateBuilder<State, StateMachineBuilder>(this, rootState, stateName);
        }

        /// <summary>
        /// Return the root state once everything has been set up.
        /// </summary>
        public IState Build()
        {
            return rootState;
        }
    }
}
