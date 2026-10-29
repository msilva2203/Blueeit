import { useState, useEffect } from "react";

import { getStats } from "../../api/stats";

import Block from "../Block";

import "./StatsPanel.css";
import "./Panel.css";

export default function StatsPanel() {
    const [stats, setStats] = useState([]);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        async function fetchStats() {
            setIsLoading(true);

            try {
                const data = await getStats();

                setStats(data);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
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
                            {isLoading ? ("-") : (stats.forumCount)}
                        </dd>
                    </dl>
                    <dl className="blueeit-panel-stats-pairs">
                        <dt>
                            Threads:
                        </dt>
                        <dd>
                            {isLoading ? ("-") : (stats.threadCount)}
                        </dd>
                    </dl>
                    <dl className="blueeit-panel-stats-pairs">
                        <dt>
                            Messages:
                        </dt>
                        <dd>
                            {isLoading ? ("-") : (stats.postCount)}
                        </dd>
                    </dl>
                    <dl className="blueeit-panel-stats-pairs">
                        <dt>
                            Members:
                        </dt>
                        <dd>
                            {isLoading ? ("-") : (stats.userCount)}
                        </dd>
                    </dl>
                    <dl className="blueeit-panel-stats-pairs">
                        <dt>
                            Latest Member:
                        </dt>
                        <dd>
                            {isLoading ? ("-") : (
                                stats.latestUser != null ? (
                                    <a href={`/members/${stats.latestUser.id}`}>{stats.latestUser.username}</a>
                                ) : ("-")
                            )}
                        </dd>
                    </dl>
                </div>
            </Block>
        </>
    );
}