import Block from "./Block";

import "./MemberBanner.css";
import Post from "./Post";

export default function MemberBanner() {
    return (
        <>
            <Block>
                <div className="blueeit-memberbanner">
                    <div className="blueeit-memberbanner-main">
                        <div className="blueeit-memberbanner-main-content">
                            <div className="blueeit-memberbanner-avatar">
                                <div className="blueeit-memberbanner-avatar-wrapper">
                                    <div className="blueeit-memberbanner-avatar-avatar">

                                    </div>
                                </div>
                            </div>
                            <div className="blueeit-memberbanner-info">
                                <h1 className="blueeit-memberbanner-username">marcosilva2203</h1>
                                <div className="blueeit-memberbanner-info-inner">
                                    <div className="blueeit-memberbanner-blurb">
                                        <dl className="blueeit-pairs inline">
                                            <dt>Joined: </dt>
                                            <dd>Sep 26, 2026</dd>
                                        </dl>
                                    </div>
                                    <div className="blueeit-memberbanner-blurb">
                                        <dl className="blueeit-pairs inline">
                                            <dt>Last seen: </dt>
                                            <dd>Today at 02:00</dd>
                                        </dl>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="blueeit-memberbanner-content">
                        <dl>
                            <dt>Messages</dt>
                            <dd>100K</dd>
                        </dl>
                    </div>
                </div>
            </Block>
        </>
    );
}