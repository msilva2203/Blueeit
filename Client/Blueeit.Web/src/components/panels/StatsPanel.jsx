import { useState, useEffect } from "react";

import Block from "../Block";

import "./StatsPanel.css";
import "./Panel.css";

const API_URL = "http://localhost:5230/api/v1";

export default function StatsPanel() {
    const [stats, setStats] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function fetchStats() {
            setLoading(true);

            try {
                const response = await fetch(
                    `${API_URL}/stats`
                );

                if (!response.ok) {
                    throw new Error("Failed to fetch stats!");
                }

                const data = await response.json();

                setStats(data);
            } catch (error) {
                console.error(error);
            } finally {
                setLoading(false);
            }
        }

        fetchStats();
    }, [])

    return (
        <>
            <Block>
                <h3 className="blueeit-panel-header">Statistics</h3>
                <div className="blueeit-panel-body">
                    <dl className="blueeit-panel-stats-pairs">
                        <dt>
                            Forums:
                        </dt>
                        <dd>
                            {loading ? (0) : (stats.forumCount)}
                        </dd>
                    </dl>
                    <dl className="blueeit-panel-stats-pairs">
                        <dt>
                            Threads:
                        </dt>
                        <dd>
                            {loading ? (0) : (stats.threadCount)}
                        </dd>
                    </dl>
                    <dl className="blueeit-panel-stats-pairs">
                        <dt>
                            Messages:
                        </dt>
                        <dd>
                            {loading ? (0) : (stats.postCount)}
                        </dd>
                    </dl>
                    <dl className="blueeit-panel-stats-pairs">
                        <dt>
                            Members:
                        </dt>
                        <dd>
                            {loading ? (0) : (stats.userCount)}
                        </dd>
                    </dl>
                </div>
            </Block>
        </>
    );
}